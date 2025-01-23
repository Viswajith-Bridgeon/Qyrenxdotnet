using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.DTOs;
using Qyrenx.Business.DTOs.Deliverypersons;
using Qyrenx.Business.DTOs.PickUpDtos;
using Qyrenx.Business.DTOs.VendorDtos;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.DbAccess.AddressRepo;
using Qyrenx.Dataccess.DbAccess.DeliveryRepo;
using Qyrenx.Dataccess.DbAccess.GadgetRepo;
using Qyrenx.Dataccess.DbAccess.Pickuprep;
using Qyrenx.Dataccess.DbAccess.StatusRepo;
using Qyrenx.Dataccess.DbAccess.UserRepo;
using Qyrenx.Dataccess.DbAccess.VendorCostRepo;
using Qyrenx.Dataccess.DbAccess.VendorRepo;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.PickupServices
{
    public class PickupServices : IPickupServices
    {
        private readonly IpickupsRepo _pickupsRepo;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;
        private readonly IuserRepo _userRepo;
        private readonly IgadgetRepo _gadgetRepo;
        private readonly IAddress _address;
        private readonly IdeliveryRepo _deliveryRepo;
        private readonly IVendorRepo _vendorRepo;
        private readonly QyrenxContext _context;
        private readonly IstatusRepo _statusRepo;
        private readonly IVendorCostRepo _vendorCostRepo;
        public PickupServices(IpickupsRepo repo,IMapper mapper, IEmailServices emailServices, IuserRepo userRepo, IgadgetRepo gadgetRepo,IAddress address, IVendorRepo vendorRepo,QyrenxContext context,IstatusRepo statusRepo, IdeliveryRepo deliveryRepo, IVendorCostRepo vendorCostRepo)
        {
            _pickupsRepo = repo;    
            _mapper = mapper;
            _emailServices = emailServices;
            _userRepo = userRepo;
            _gadgetRepo = gadgetRepo;
            _address = address;
            _vendorRepo = vendorRepo;
            _context = context;
            _statusRepo = statusRepo;
            _deliveryRepo = deliveryRepo;
            _vendorCostRepo = vendorCostRepo;

        }

        public async Task<List<PickupDeliveryDto>> GetPickupsDeliveryBoys(Guid id)
        {
            try
            {
                var data = await _pickupsRepo.GetAllPickup();
                var pick=data.Where(p=>p.DeliveryPersonId==id).ToList();
                
                return _mapper.Map<List<PickupDeliveryDto>>(pick);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<string>VerifyPickup(Guid id,Guid userid)
        {
            try
            {
                var data= await _pickupsRepo.GetPickupById(id);
                var status=await _context.Status.Where(e=>e.PickupId==data.Id).ToListAsync();
                var lastStatus=status.OrderByDescending(e=>e.CreatedOn).FirstOrDefault();
               if(lastStatus.Statuss== "payment successfull waiting for Deliveryperson")
                {
                    if (data.DeliveryPersonId == userid)
                    {
                            var gad = await _gadgetRepo.GetordergadgetsById(data.GadgetId);
                            var user = await _userRepo.GetUserById(gad.UserId);
                            var delivery = await _deliveryRepo.GetDeliveryPeresonById(userid);

                            var mail_send =await  _emailServices.SendOtpForDeliveryBoyVerification(user.Email);
                            if (mail_send)
                            {
                                return "verification suucessfully completed and sending otp";
                            }
                            return "something wrong in email";
                       
                    }

                    return "not is delveryboy";
                }
                return "already verified";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }


        public async Task<LatLong> LatLongOfUser(Guid id)
        {
            try
            {
             
                var data = await _pickupsRepo.GetPickupById(id);
                if (data == null)
                    throw new Exception("Pickup not found for the given ID.");

                var gad = await _gadgetRepo.GetordergadgetsById(data.GadgetId);
                if (gad == null)
                    throw new Exception("Gadget not found for the given Gadget ID.");

                var addrs = await _address.GetAddressByAddId(gad.AddressId);
                if (addrs == null)
                    throw new Exception("Address not found for the given Address ID.");

                return await GetCoordinatesFromAddress(addrs);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in LatLongOfUser: {ex.Message}", ex);
            }
        }

        public async Task<LatLong> GetCoordinatesFromAddress(Address address)
        {
            try
            {
                var fullAddress = GetFullAddress(address);
                if (string.IsNullOrWhiteSpace(fullAddress))
                    throw new Exception("Invalid address provided.");

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("YourAppName/1.0");

                var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(fullAddress)}&format=json";
                var response = await httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to fetch coordinates. HTTP Status: {response.StatusCode}. Error: {errorContent}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<GeoResponse>>(json);
                if (data == null || !data.Any())
                    throw new Exception($"No coordinates found for the address: {fullAddress}.");

                // Filter results for the most relevant one (e.g., type == "town")
                var mostRelevantResult = data.FirstOrDefault(d => d.Type == "town");
                if (mostRelevantResult == null)
                    mostRelevantResult = data.First(); // Fallback to the first result if no "town" type is found

                if (!decimal.TryParse(mostRelevantResult.Lat, out var lat) || !decimal.TryParse(mostRelevantResult.Lon, out var lon))
                    throw new Exception("Failed to parse latitude or longitude from the response.");

                return new LatLong
                {
                    Lat = lat,
                    Lon = lon   
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetCoordinatesFromAddress: {ex.Message}", ex);
            }
        }

        private string GetFullAddress(Address address)
        {
            if (address == null)
                return string.Empty;
            return $"{address.City}, {address.PostalCode}";
        }


       public async  Task<bool> SendFormToUser(Guid ven_id, VendorCostDto details)
        {
            try
            {
                var exist_ven=await _vendorRepo.GetVendorById(ven_id);
                var StatusCheck=await _statusRepo.GetStatusByPickId(details.PickupId);
                var lastStatus = StatusCheck.OrderByDescending(e => e.CreatedOn).FirstOrDefault();

                if (lastStatus.Statuss == "Start Checking") 
                {
                    var add_vendor_cost = new VendorCost
                    {
                        VendorId = ven_id,
                        PickupId = details.PickupId,
                        ProblemDescription = details.ProblemDescription,
                        IsVenorServiceable = details.IsVendorServiceable,
                        SaleCost = details.SaleCost,
                        CreatedBy = exist_ven.Name,
                        ServiceCost = details.ServiceCost,
                    };
                    var vendor_cast = _mapper.Map<VendorCost>(add_vendor_cost);
                    await _context.VendorCost.AddAsync(vendor_cast);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;    

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }





        public async Task<string> pickupVerificationofUser(Guid pid, string otp)
        {
            try
            {
                var pick=await _pickupsRepo.GetPickupById(pid);
                if(pick==null)
                {
                    return "invalid pickup id";
                }
                var gad=await _gadgetRepo.GetordergadgetsById(pick.GadgetId);
                var user=await _userRepo.GetUserById(gad.UserId);
                bool verify =await _emailServices.UserToDeliverPersonVerifyOtp(user.Email, otp);
                if (verify)
                {
                    var status = new Status
                    {
                        PickupId = pick.Id,
                        Statuss = "DeliveryPerson Recevied Successfully"
                    };
                    await _context.Status.AddAsync(status);
                    await _context.SaveChangesAsync();
                    return "DeliveryPerson Recevied Successfully";
                }
                return "invalid user email";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }





        public async Task<List<PickupVendorDto>> GetPickupsVendor(Guid id)
        {
            try
            {
                var data = await _pickupsRepo.GetAllPickup();
                var pick = data.Where(p => p.VendorId == id).ToList();

                return _mapper.Map<List<PickupVendorDto>>(pick);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }


        public async Task<string> VerifyPickupByDeliveryboyToVendor(Guid pid, Guid userid)
        {
            try
            {
                var data = await _pickupsRepo.GetPickupById(pid);
                var status = await _context.Status.Where(e => e.PickupId == data.Id).ToListAsync();
                var lastStatus = status.OrderByDescending(e => e.CreatedOn).FirstOrDefault();
                if (lastStatus.Statuss == "DeliveryPerson Recevied Successfully")
                {
                    
                   var gad = await _gadgetRepo.GetordergadgetsById(data.GadgetId);
                   var vendor = await _vendorRepo.GetVendorById(data.VendorId);
                    var mail_send =await  _emailServices.SendOtpForVendorVerification(vendor.Email);
                    if (mail_send)
                    {
                        return "suucessfully verified and sending otp";
                    }
                     return "invalid vendor email";
                   
                }
                return "Already verified";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }




        public async Task<bool> pickupVerificationofVendor(Guid pid, string otp)
        {
            try
            {
                var pick = await _pickupsRepo.GetPickupById(pid);
                if (pick == null)
                {
                    return false;
                }
                var vendor=await _vendorRepo.GetVendorById(pick.VendorId);
                bool verify = await _emailServices.UserToDeliverPersonVerifyOtp(vendor.Email, otp);
                if (verify)
                {
                    var status1 = new Status
                    {
                        PickupId = pick.Id,
                        Statuss = "Vendor Recevied Successfully"
                    };
                    var status2 = new Status
                    {
                        PickupId = pick.Id,
                        Statuss = "Start Checking"
                    };
                    await _context.Status.AddAsync(status1);
                    await _context.Status.AddAsync(status2);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetCoordinatesFromAddress: {ex.Message}", ex);
            }
        }




        public async Task<VendorCostView> GetSeviceDetialsByPickup(Guid userid, Guid pickupid)
        {
            try
            {
                var user = await _userRepo.GetUserById(userid);
                if (user != null)
                {
                    var ved_cost = await _vendorCostRepo.GetVendorCostByPickup(pickupid);
                    var vendor = await _vendorRepo.GetVendorById(ved_cost.VendorId);
                    var vendor_cost_view = new VendorCostView
                    {
                        Id=ved_cost.Id,
                        VendorName = vendor.Name,
                        SaleCost = ved_cost.SaleCost,
                        ProblemDescription = ved_cost.ProblemDescription,
                        IsVendorServiceable = ved_cost.IsVenorServiceable,
                        PickupId = ved_cost.PickupId,
                        VendorPhone = vendor.Mobile,
                        ServiceCost = ved_cost.ServiceCost,
                        IsService=ved_cost.IsServices
                    };
                    return vendor_cost_view;
                }
                return new VendorCostView();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }




        public async Task<ICollection<PickUpDto>> GetPickupsUserId(Guid id)
        {
            try
            {
                var gadget = await _gadgetRepo.GetgadgetsByUserId(id);
                List<Pickup> pickids = new List<Pickup>();
                foreach(var gad in gadget)
                {
                    var pick=await _pickupsRepo.GetPickupByGadId(gad.Id);
                    if(pick!=null)
                    {
                        pickids.Add(pick);
                    }
                }
                return _mapper.Map<ICollection<PickUpDto>>(pickids);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }




        public async Task<string> UserApproveService(Guid Vc_id)
        {
            try
            {
                var vendorcost =await _vendorCostRepo.GetVendorCostById(Vc_id);
                if(vendorcost==null)
                {
                    return "invalid Id";
                }
                if (vendorcost.IsServices == true)
                {
                    return "already approved";
                }
                vendorcost.IsServices=true;
                var status1 = new Status
                {
                    PickupId = vendorcost.PickupId,
                    Statuss = "Start Services"
                };
                await _context.Status.AddAsync(status1);
                await _context.SaveChangesAsync();
                return "accepted Services";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

      

        public async Task<ICollection<PickUpDto>> UserApprovedService(Guid Vcid)
        {
            try
            {
                var pickups = await _pickupsRepo.GetPickupByVendorId(Vcid);
                List<Pickup> pickids = new List<Pickup>();
                if (pickups!=null)
                {
                   foreach(var picks in pickups)
                    {
                        var vc=await _vendorCostRepo.GetVendorCostByPickup(picks.Id);
                        if (vc != null && vc.IsServices==true)
                        {
                            pickids.Add(picks);
                        }

                    }
                    return _mapper.Map<ICollection<PickUpDto>>(pickids);

                }
                return new List<PickUpDto>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
