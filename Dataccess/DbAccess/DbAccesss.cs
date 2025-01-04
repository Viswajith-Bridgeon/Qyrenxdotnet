using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess
{
    public class DbAccesss:IDbAccess
    {
        private readonly QyrenxContext _context;
        public DbAccesss(QyrenxContext qyrenxContext)
        {
            _context = qyrenxContext;
        }
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var data = await _context.Users.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<Vendor>> GetAllVendor()
        {
            try
            {
                var data = await _context.Vendors.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<DeliveryPerson>> GetAllDeliveryPerson()
        {
            try
            {
                var data = await _context.DeliveryPersons.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<List<DeliveryPersonOnline>> GetAllDeliveryPersonOnline()
        {
            try
            {
                var data=await _context.DeliveryPersonOnlines.ToListAsync();
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<List<Gadget>> GetAllGadgets()
        {
            try
            {
                var data = await _context.Gadgets.ToListAsync(); 
                return data;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<OrderGadget>> GetAllOrdersGadgets()
        {
            try
            {
                var data=await _context.OrderGadgets.ToListAsync();
                return data;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<PaymentToUser>> GetAllPaymentToUser()
        {
            try
            {
                var data = await _context.PaymentToUsers.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<Pickup>> GetAllPickups()
        {
            try
            {
                var data = await _context.Pickups.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<Status>> GetAllStatus()
        {
            try
            {
                var data = await _context.Status.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<VendorAddress>> GetAllVendorAddresses()
        {
            try
            {
                var data = await _context.VendorAddresses.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }
        public async Task<List<VendorCategory>> GetAllVendorCategories()
        {
            try
            {
                var data = await _context.VendorCategories.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<List<UserSecurityPayment>> GetAllUserSecurityPayment()
        {
            try
            {
                var data = await _context.UserPayment.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<List<Address>> GetAllAddressAddresses()
        {
            try
            {
                var data = await _context.Address.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                var data =await _context.Categories.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

    }
}
