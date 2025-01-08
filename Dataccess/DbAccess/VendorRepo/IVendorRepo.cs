using Microsoft.AspNetCore.Http;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.VendorRepo
{
    public interface IVendorRepo
    {
        Task<bool> LoginVendor(string email,string pass);
        Task<IEnumerable<Vendor>> GetVendor();
        Task<IEnumerable<Vendor>> GetVendorNotVerified();
        Task<Vendor> GetVendorById(Guid id);
        Task<string>VendorRegistration(Vendor vendor);
        //Task<IEnumerable<Vendor>> GetVendorByShopeName(string name);
        Task<Vendor> GetVendorByMail(string mail);
        Task<bool> BlockOrUnblockVendor(Guid id);
        Task<bool> VerificationVendor(Guid id);
        Task<bool> CategoryAddvendor(Guid id, Guid catid);
        Task<bool> AddVendorAddress(VendorAddress address, Vendor exist);
    

    }
}
