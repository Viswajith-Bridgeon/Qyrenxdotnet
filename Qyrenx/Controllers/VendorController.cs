using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Models.DTOs.VendorDtos;
using Qyrenx.Services.VendorServices;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorServices _vendorServices;

        public VendorController(IVendorServices vendorServices)
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
				var vendore = await _vendorServices.RegisterVendor(vendorRegister, shopelicense);
				if (vendore)
				{
					return Ok("Registeration Is In Verification");
				}
				return Ok("There was an error in Registration");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
