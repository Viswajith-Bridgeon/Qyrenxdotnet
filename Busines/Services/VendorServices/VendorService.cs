using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qyrenx.Business.Services.CloudinaryService;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Business.Services.JwtServices;
using Qyrenx.Dataccess.Models.Entities;
using Qyrenx.Business.Models.DTOs.VendorDtos;
using Qyrenx.Dataccess.ApplicationDbContext;

namespace Qyrenx.Business.Services.VendorServices
{
    public class VendorService : IVendorServices
	{
		private readonly QyrenxContext _context;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly ICloudinaryService _cloudinaryService;
		private readonly IJwtService _jwtService;
		private readonly IEmailServices _emailServices;
        public VendorService(QyrenxContext context,ICloudinaryService cloudinaryService,IMapper mapper,IJwtService jwtService,IEmailServices emailServices)
        {
            _context = context;
			_mapper = mapper;
			_cloudinaryService= cloudinaryService;
			_jwtService=jwtService;
			_emailServices=emailServices;
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
				Email = v.Email,
                IsBlock=v.IsBlock,
			});
			return vendor.ToList();
		}

		public async Task<VendorAdminViewDto> GetVendorById(Guid id)
		{
			try
			{
				var exist = await _context.Vendors.FirstOrDefaultAsync(e=>e.Id==id);
				if (exist == null)
					return new VendorAdminViewDto { };
				var vendor = _mapper.Map<VendorAdminViewDto>(exist);
				return vendor;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
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
				Email = v.Email,
                IsBlock= v.IsBlock,
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
				Email = v.Email,
                IsBlock= v.IsBlock,
			});
			return vendor.ToList();
		}

		public async Task<VendorLoginView> LoginVendor(VendorLogin loginDto)
		{
			try
			{
				var exist = await _context.Vendors.SingleOrDefaultAsync(p => p.Email == loginDto.Email);
				if (exist != null)
				{
					if (BCrypt.Net.BCrypt.Verify(loginDto.Password, exist.HashPassword))
					{
						if (exist.IsVerified == true)
						{
							if (exist.IsBlock == false)
							{
								var token = _jwtService.GenerateJwt(exist.Id, exist.Email, exist.Role);
								return new VendorLoginView { Name = exist.Name, Id = exist.Id, Token = token };
							}
							return new VendorLoginView { Error = "person is blocked " };
						}
						return new VendorLoginView { Error = "persons verification is pending!" };
					}
					return new VendorLoginView { Error = "enter valid credentials" };
				}
				return new VendorLoginView { Error = "no such vendor person is registered" };
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException.Message);
			}
		}

		public async Task<string> RegisterVendor(VendorRegisterDto registerDto, IFormFile shopelicense)
		{
			try
			{

				var exist = await _context.Vendors.FirstOrDefaultAsync(c => c.Email == registerDto.Email);
				if (exist != null)
				{
					return "vendor already exist";
				}
				bool emailverify = _emailServices.verifyOtp(registerDto.Email, registerDto.otp);
				if (emailverify)
				{
					var license = await _cloudinaryService.UploadDocumentAsync(shopelicense);
					var vendor = _mapper.Map<Vendor>(registerDto);
					vendor.HashPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
					vendor.ShopeLicense = license;
					_context.Vendors.Add(vendor);
					await _context.SaveChangesAsync();
					return "registered successfully";
				}
				return "wrong otp";
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
				var vendor = await _context.Vendors.FirstOrDefaultAsync(e=>e.Id==id);
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
				await _emailServices.SendVerifiedmsg(vendor.Role,vendor.Name,vendor.Email);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("There was an ERORR in VERIFICATION");
			}
		}
	}
}
