
using Microsoft.AspNetCore.Http;
using Qyrenx.Business.DTOs;
using Qyrenx.Business.Models.DTOs.VendorDtos;
using Qyrenx.Dataccess.Models.Entities;

namespace Qyrenx.Business.Services.VendorServices
{
	public interface IVendorServices
	{
		Task<string> RegisterVendor(VendorRegisterDto registerDto, IFormFile shopelicense);
		Task<AllLoginresponses> LoginVendor(VendorLogin loginDto);
		Task<IEnumerable<VendorAdminViewDto>> GetVendor();
		Task<IEnumerable<VendorAdminViewDto>> GetVendorNotVerified();
		Task<VendorAdminViewDto> GetVendorById(Guid id);
		//Task<IEnumerable<VendorAdminViewDto>> GetVendorByShopeName(string name);
        Task<bool> BlockOrUnblockVendor(Guid id);
        Task<bool> VerificationVendor(Guid id);
        Task<Guid> VendorAssign(Guid catid);
        Task<bool> CategoryAddvendor(Guid id, Guid catid);
        Task<VendorCategory> ViewCategory(Guid id);
        Task <VendorOnline> VendorActivity(Guid id);
        Task<Guid> GetNearestVendorPerson(Guid id);
        Task<bool> ResetPassword(string Email, string password);
        Task<bool> VendorAssignDeliveryPerson(Guid venid, Guid pickupid);
    }
}
