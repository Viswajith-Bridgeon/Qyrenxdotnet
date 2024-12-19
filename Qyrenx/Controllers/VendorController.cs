using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.ApiResponses;
using Qyrenx.Models.DTOs.VendorDtos;
using Qyrenx.Models.Entities;
using Qyrenx.Services.EmailServices;
using Qyrenx.Services.VendorServices;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorServices _vendorServices;
		private readonly IEmailServices _emailServices;

        public VendorController(IVendorServices vendorServices,IEmailServices emailServices)
        {
            _vendorServices = vendorServices;
        }

		[HttpPut("block/{id}")]
		public async Task<ActionResult<string>> Block(Guid id)
		{
			try
			{
				var vendor = await _vendorServices.BlockVendor(id);
				if (vendor)
				{
					return Ok("Blocked Successfully");
				}
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("unblock/{id}")]
		public async Task<ActionResult<string>> UnBlock(Guid id)
		{
			try
			{
				var vendor = await _vendorServices.UnblockVendor(id);
				if (vendor)
				{
					return Ok("UnBlocked Successfully");
				}
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPut("verification")]
		public async Task<ActionResult<string>> Varification(Guid id)
		{
			try
			{
				var vendor = await _vendorServices.VerificationVendor(id);
				if (vendor)
				{
					return Ok("Verified Successfully");
				}
				return NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<VendorAdminViewDto>> GetVendors()
		{
			try
			{
				var res = await _vendorServices.GetVendor();
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest("");
			}
		}

		[HttpGet("notverified")]
		public async Task<ActionResult<VendorAdminViewDto>> GetVendorsNotverified()
		{
			try
			{
				var res = await _vendorServices.GetVendorNotVerified();
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<VendorAdminViewDto>> GetVendor(Guid id)
		{
			try
			{
				var res = await _vendorServices.GetVendorById(id);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpGet("shopename")]
		public async Task<ActionResult<VendorAdminViewDto>> GetVendorsByName(string shopename)
		{
			try
			{
				var res = await _vendorServices.GetVendorByShopeName(shopename);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpPost("register")]
		public async Task<ActionResult<string>> Register([FromForm] VendorRegisterDto vendorRegister, IFormFile shopelicense)
		{
			try
			{
				var res = await _vendorServices.RegisterVendor(vendorRegister, shopelicense);

				if (res == "vendor already exist")
				{
					var r = new ApiResponse<string>(409, "Vendor already exist");
					return Conflict(r);
				}
				if (res == "wrong otp")
				{
					var r = new ApiResponse<string>(400, "Wrong OTP");
					return BadRequest(r);
				}
				return Ok(new ApiResponse<string>(200, res));
			}
			catch (Exception ex)
			{
				var r = new ApiResponse<string>(500, "server error", null, ex.Message);
				return StatusCode(500, r);
			}
		}

		[HttpPost("login")]
		public async Task<ActionResult<VendorLoginView>> Login(VendorLogin vendorLogin)
		{
			if (vendorLogin == null)
			{
				return BadRequest("Enter credentials");
			}
			var vendor = await _vendorServices.LoginVendor(vendorLogin);
			if (vendor != null)
			{
				return Ok(vendor);
			}
			return NoContent();
		}

	}
}
