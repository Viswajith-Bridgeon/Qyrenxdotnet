using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Crypto.Engines;
using Qyrenx.Business.DTOs.UserSecurityPaymentDto;
using Qyrenx.Business.Services.DeliveryServices;
using Qyrenx.Business.Services.VendorServices;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.DbAccess;
using Qyrenx.Dataccess.DbAccess.GadgetRepo;
using Qyrenx.Dataccess.DbAccess.UserSecurityPay;
using Qyrenx.Dataccess.Models.Entities;
using Razorpay.Api;
using System;

namespace Qyrenx.Business.Services.UserSecurityPay
{
    public class UserSecurityPaymentService : IUserSecurityPaymentService
    {
        private readonly QyrenxContext _context; 
        private readonly IConfiguration _configuration;
        private readonly IVendorServices _vendorServices;
        private readonly IDeliveryService _deliveryService;
        private readonly IgadgetRepo _gadgetRepo;
        private readonly IuserSecurityRepo _userSecurityRepo;
        public UserSecurityPaymentService(QyrenxContext context,IConfiguration configuration, IVendorServices vendorServices,IDeliveryService delivery,IgadgetRepo gadgetRepo,IuserSecurityRepo userSecurityRepo)
        {
            _configuration = configuration;
            _context = context;
            _vendorServices = vendorServices;
            _deliveryService = delivery;
            _gadgetRepo = gadgetRepo;
            _userSecurityRepo = userSecurityRepo;
        }

        public async Task<string> RazorOrderCreate(long price)
        {
            try
            {
                if (price <= 0)
                {
                    throw new Exception("price must be a positive value");
                }
                Dictionary<string, object> razorinpt = new Dictionary<string, object>();
                string transactionId = Guid.NewGuid().ToString();
                razorinpt.Add("amount", Convert.ToDecimal(price) * 100);
                razorinpt.Add("currency", "INR");
                razorinpt.Add("receipt", transactionId);

                string key = _configuration["Razorpay:KeyId"];
                string secret = _configuration["Razorpay:KeySecret"];

                RazorpayClient client = new RazorpayClient(key, secret);
                Razorpay.Api.Order order = client.Order.Create(razorinpt);
                var orderId = order["id"].ToString();
                return orderId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Razorpay order"+ ex.InnerException.Message);
            }
        }

        public bool PaymentVerify(UserSecurityRazorDto razorDto)
        {
            try
            {
                if (razorDto == null || razorDto.razorpay_order_id == null || razorDto.razorpay_payment_id == null || razorDto.razorpay_signature == null)
                {
                    return false;
                }
                RazorpayClient client = new RazorpayClient(_configuration["Razorpay:KeyId"], _configuration["Razorpay:KeySecret"]);
                Dictionary<string, string> input = new Dictionary<string, string>();
                input.Add("razorpay_payment_id", razorDto.razorpay_payment_id);
                input.Add("razorpay_order_id", razorDto.razorpay_order_id);
                input.Add("razorpay_signature", razorDto.razorpay_signature);
                Utils.verifyPaymentSignature(input);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Payment verification error"+ex.InnerException.Message);
            }
        }
        


        public async Task<bool> CreateOrder(Guid id, UserSecurityInputDto inputorderDto)
        {
            if (id == Guid.Empty)
            {
                throw new Exception("User not found");
            }

            var executionStrategy = _context.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                // Start a transaction
                await using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // Check the number of gadgets for the user
                    var data =await _gadgetRepo.Getgadgets();
                    var count = data.Count(c => c.UserId == id);
                    Gadget gad;
                    
                    // Fetch the gadget based on count
                    if (count <= 1)
                    {
                       
                       gad=data.FirstOrDefault(c => c.UserId == id);
                    }
                    else
                    {
                        gad = data
                            .Where(c => c.UserId == id)
                            .OrderBy(c => c.CreatedOn)
                            .LastOrDefault();
                    }

                    if (gad == null)
                    {
                        throw new Exception("User has no gadgets");
                    }

                    // Create a new order and payment record
                    var order = new UserSecurityPayment
                    {
                        UserId = id,
                        SecurityAmount = 500 * 1000,
                        PaymentString = inputorderDto.PaymentString,
                        TransactionId = inputorderDto.TransactionId,
                    };

                    await _context.UserPayment.AddAsync(order);
                    await _context.SaveChangesAsync();

                    // Retrieve the payment object to link with the gadget order
                    var pay = await _userSecurityRepo.GetAllUserSecurityPayment();
                    var payid = pay.FirstOrDefault(c =>
                        c.TransactionId == inputorderDto.TransactionId &&
                        c.PaymentString == inputorderDto.PaymentString);

                    if (payid == null)
                    {
                        throw new Exception("Payment record not found");
                    }

                    var ordergad = new OrderGadget
                    {
                        GadgetId = gad.Id,
                        price = 500 * 1000,
                        PaymentId = payid.Id,
                    };

                    await _context.OrderGadgets.AddAsync(ordergad);
                    await _context.SaveChangesAsync();

                    // Check if VendorId is valid
                    //var vendorId = await _vendorServices.VendorAssign(gad.CategoryId);
                    //if (vendorId == null)
                    //{
                    //    throw new Exception("No valid vendor found for the given category");
                    //}

                    // Check if DeliveryPersonId is valid
                    var deliveryPersonId = await _deliveryService.GetNearestDeliveryPerson(gad.AddressId);
                    var vendorid = await _vendorServices.GetNearestVendorPerson(gad.AddressId);
                    if (deliveryPersonId == null||vendorid==null)
                    {
                        throw new Exception("No valid delivery person or vendor found for the given address");
                    }

                    // Ensure DeliveryPersonId exists in the DeliveryPersons table
                    var person =await _deliveryService.GetAllDeliveryPersonOnline();
                    var deliveryPersonExists = person
                    .FirstOrDefault(u => u.DeliveryPersonId == deliveryPersonId);
                    if (deliveryPersonExists==null)
                    {
                        throw new Exception($"Delivery person with ID {deliveryPersonId} does not exist.");
                    }

                    var pick = new Pickup
                    {
                        GadgetId = ordergad.GadgetId,
                        VendorId = vendorid,
                        DeliveryPersonId = deliveryPersonId
                    };

                    await _context.Pickups.AddAsync(pick);
                    await _context.SaveChangesAsync();
                    var status = new Status
                    {
                        PickupId = pick.Id,
                        Statuss = "payment successfull waiting for Deliveryperson"
                    };
                    await _context.Status.AddAsync(status);
                    await _context.SaveChangesAsync();
                    // Commit the transaction if all operations are successful
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of an error
                    await transaction.RollbackAsync();
                    throw new Exception("Error creating Razorpay order: " + ex.Message);
                }
            });
        }





        public async Task<IEnumerable<UserSecurityPaymentViewDto>> GetUserOrder(Guid id)
        {
            throw new NotImplementedException();
        }

       

       
    }
}
