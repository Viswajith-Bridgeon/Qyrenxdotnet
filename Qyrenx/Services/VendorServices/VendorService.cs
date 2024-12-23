using System.ComponentModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qyrenx.ApplicationDbContext;
using Qyrenx.Models.DTOs.VendorDtos;
using Qyrenx.Models.Entities;
using Qyrenx.Services.CloudinaryService;

namespace Qyrenx.Services.VendorServices
{
	public class VendorService : IVendorServices
	{
		private readonly QyrenxContext _context;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly ICloudinaryService _cloudinaryService;

        public VendorService(QyrenxContext context,ICloudinaryService cloudinaryService,IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
			_cloudinaryService= cloudinaryService;
        }
       

		public async Task<IEnumerable<VendorAdminViewDto>> GetVendor()
		{
			var vendors = _context.Vendors;
			var vendor = vendors.Select(v => new VendorAdminViewDto
			{
				Id = v.Id,
				Name = v.Name,
				ShopeName = v.ShopeName,
				ShopeLicense = Path.ChangeExtension(v.ShopeLicense, ".jpg"),
				Mobile = v.Mobile,
				Date = v.Date,
				Email = v.Email,
			});
			return vendor.ToList();
		}

		public async Task<VendorAdminViewDto> GetVendorById(Guid id)
		{
			try
			{
				var exist = await _context.Vendors.FindAsync(id);
				if (exist == null)
					return new VendorAdminViewDto { };
				var vendor = _mapper.Map<VendorAdminViewDto>(exist);
				return vendor;
			}
			catch (Exception ex)
			{
				throw new Exception("vendoreid is not valid");
			}
		}

		public async Task<IEnumerable<VendorAdminViewDto>> GetVendorByShopeName(string name)
		{
			var vendors = _context.Vendors.Where(c => c.ShopeName.ToLower().Contains(name.ToLower()));
			if (!vendors.Any())
			{
				return Enumerable.Empty<VendorAdminViewDto>();
			}
			var vendor = vendors.Select(v => new VendorAdminViewDto
			{
				Id = v.Id,
				Name = v.Name,
				ShopeName = v.ShopeName,
				ShopeLicense = Path.ChangeExtension(v.ShopeLicense, ".jpg"),
				Mobile = v.Mobile,
				Date = v.Date,
				Email = v.Email,
			});
			return vendor.ToList();
		}

		public async Task<IEnumerable<VendorAdminViewDto>> GetVendorNotVerified()
		{
			var vendors = _context.Vendors.Where(v => v.IsVerified == false);
			var vendor = vendors.Select(v => new VendorAdminViewDto
			{
				Id = v.Id,
				Name = v.Name,
				ShopeName = v.ShopeName,
				ShopeLicense = Path.ChangeExtension(v.ShopeLicense, ".jpg"),
				Mobile = v.Mobile,
				Date = v.Date,
				Email = v.Email,
			});
			return vendor.ToList();
		}

		public Task<VendorLoginView> LoginVendor(VendorLogin loginDto)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> RegisterVendor(VendorRegisterDto registerDto, IFormFile shopelicense)
		{
			try
			{
				var license = await _cloudinaryService.UploadDocumentAsync(shopelicense);

				var exist = await _context.Vendors.FirstOrDefaultAsync(c => c.Email == registerDto.Email);
				if (exist != null)
				{
					return false;
				}
				var vendor = _mapper.Map<Vendor>(registerDto);
				//vendor.Role = "Vendor";
				vendor.HashPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
				vendor.ShopeLicense = license;
				_context.Vendors.Add(vendor);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Registration Failed ,MSG {ex}");
			}
		}
		public async Task<bool> BlockVendor(Guid id)
		{
			try
			{
				var vendor = await _context.Vendors.FindAsync(id);
				if (vendor == null)
				{
					return false;
				}
				vendor.IsBlock = true;
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("There was an ERORR in block");
			}
		}
		public async Task<bool> UnblockVendor(Guid id)
		{
			try
			{
				var vendor = await _context.Vendors.FindAsync(id);
				if (vendor == null)
				{
					return false;
				}
				vendor.IsBlock = false;
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("There was an ERORR in unblock");
			}
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
				vendor.IsVerified= true;
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
