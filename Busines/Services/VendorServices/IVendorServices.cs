
using Microsoft.AspNetCore.Http;
using Qyrenx.Business.Models.DTOs.VendorDtos;

namespace Qyrenx.Business.Services.VendorServices
{
	public interface IVendorServices
	{
		Task<string> RegisterVendor(VendorRegisterDto registerDto, IFormFile shopelicense);
		Task<VendorLoginView> LoginVendor(VendorLogin loginDto);
		Task<IEnumerable<VendorAdminViewDto>> GetVendor();
		Task<IEnumerable<VendorAdminViewDto>> GetVendorNotVerified();
		Task<VendorAdminViewDto> GetVendorById(Guid id);
		Task<IEnumerable<VendorAdminViewDto>> GetVendorByShopeName(string name);
		Task<bool> BlockVendor(Guid id);
		Task<bool> UnblockVendor(Guid id);
		Task<bool> VerificationVendor(Guid id);
	}
}
