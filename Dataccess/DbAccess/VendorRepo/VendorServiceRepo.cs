using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.VendorRepo
{
    public class VendorServiceRepo:IVendorRepo
    {
        private readonly QyrenxContext _context;
        public VendorServiceRepo(QyrenxContext context)
        {
            _context = context;
        }

        public async Task<bool> BlockOrUnblockVendor(Guid id)
        {
            var exist = await _context.Vendors.FirstOrDefaultAsync(x => x.Id == id);
            if (exist != null)
            {
                exist.IsBlock = !exist.IsBlock;
                _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> CategoryAddvendor(Guid id, Guid catid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Vendor>> GetVendor()
        {
            try
            {
                var vendors=await _context.Vendors.ToListAsync();
                return vendors;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }

        public async Task<Vendor> GetVendorById(Guid id)
        {
            try
            {
                var vendor_id = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
                if (vendor_id != null)
                    return vendor_id;
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<Vendor> GetVendorByMail(string email)
        {
            try
            {
                var data = await _context.Vendors.FirstOrDefaultAsync(e => e.Email == email);
                if(data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<IEnumerable<Vendor>> GetVendorNotVerified()
        {
            try
            {
                var vendors = _context.Vendors.Where(v => v.IsVerified == false);
                if(vendors.Any())
                     return vendors;
                return null;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<bool> LoginVendor(string email, string pass)
        {
            throw new NotImplementedException();
        }

     

       

        public async Task<bool> VerificationVendor(Guid id)
        {
            try
            {
                var vendor = await _context.Vendors.FindAsync(id);
                if (vendor == null)
                {
                    return false;
                }
                vendor.IsVerified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an ERORR in VERIFICATION");
            }
        }
        public async Task<string>VendorRegistration(Vendor vendor)
        {
            try
            {
                vendor.HashPassword = BCrypt.Net.BCrypt.HashPassword(vendor.HashPassword);
                vendor.ShopeLicense = vendor.ShopeLicense;
                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();
                return "success!";
            }
            catch (Exception ex)
            {
                throw new Exception("There was an ERORR in VERIFICATION");
            }

        }
        public async Task<bool> AddVendorAddress(VendorAddress address,Vendor exist)
        {
            try
            {
                address.Role = "Vendor";
                address.VendorId = exist.Id;
                _context.VendorAddresses.Add(address);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an ERORR in VERIFICATION");
            }
        }

    }
}
