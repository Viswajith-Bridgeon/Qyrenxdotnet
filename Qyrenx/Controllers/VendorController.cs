

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.Models.DTOs.VendorDtos;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Business.Services.JwtServices;
using Qyrenx.Business.Services.VendorServices;
using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.ApplicationDbContext;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Qyrenx.present.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorServices _vendorServices;
		private readonly IEmailServices _emailServices;
        private readonly QyrenxContext _context;
        private readonly IJwtService _jwtService;

        public VendorController(IVendorServices vendorServices,IEmailServices emailServices,QyrenxContext context,IJwtService jwtService)
        {
            _vendorServices = vendorServices;
            _emailServices = emailServices;
            _context = context;
        }

		//[HttpPut("block/{id}")]
		//public async Task<ActionResult<string>> Block(Guid id)
		//{
		//	try
		//	{
		//		var vendor = await _vendorServices.BlockVendor(id);
		//		if (vendor)
		//		{
		//			return Ok("Blocked Successfully");
		//		}
		//		return NotFound();
		//	}
		//	catch (Exception ex)
		//	{
		//		return BadRequest(ex.Message);
		//	}
		//}

		[HttpPut("blockunblock/{id}")]
		public async Task<ActionResult<string>> BlockOrUnblock(Guid id)
		{
			try
			{
				var vendor = await _vendorServices.BlockOrUnblockVendor(id);
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
				return StatusCode(500,ex.Message);
			}
		}

		//[HttpGet("shopename")]
		//public async Task<ActionResult<VendorAdminViewDto>> GetVendorsByName(string shopename)
		//{
		//	try
		//	{
		//		var res = await _vendorServices.GetVendorByShopeName(shopename);
		//		return Ok(res);
		//	}
		//	catch (Exception ex)
		//	{
		//		return NotFound();
		//	}
		//}

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

        [Authorize(Roles = "Vendor")]
        [HttpPost("addcategory")]
        public async Task<IActionResult> AddCategory_Vendor(Guid catid)
        {
            try
            {
                var userIdResult = GetUserIdFromClaims();
                var userId = userIdResult.Value;
                var res = await _vendorServices.CategoryAddvendor(userId, catid);
                if (res)
                {
                    return Ok("Category Added Successfully");
                }
                return BadRequest("Error");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }

        }

        private ActionResult<Guid> GetUserIdFromClaims()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdString, out Guid userId))
            {
                return userId;
            }

            return Unauthorized();
        }


        [HttpPost("refreshOfVendor")]
        public async Task<IActionResult> AccessTokenRefresh(string Refresh)
        {
            try
            {
                var user = await _context.Vendors.FirstOrDefaultAsync(e => e.RefreshToken == Refresh);
                if (user == null || user.TokenExpiryTime <= DateTime.UtcNow)
                {
                    return Unauthorized("Invalid or expired refresh token.");
                }


                var token = _jwtService.GenerateJwt(user.Id, user.Email, user.Role);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);

            }
        }

        [HttpPost("Reset-Password-Otp")]

        public async Task<IActionResult> ResetPasswordOtp(string email)
        {
            try
            {

                bool isotpset = await _emailServices.ResetPasswordOtp(email);
                if (!isotpset)
                {
                    var r = new ApiResponse<bool>(404, "Invalid Email", isotpset);
                    return NotFound(r);
                }

                var res = new ApiResponse<bool>(200, "otp sending", isotpset);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
                return StatusCode(500, r);
            }
        }


        [HttpPost("Reset-Password-Verify")]

        public async Task<IActionResult> ResetPasswordVerify(string email, string otp)
        {
            try
            {

                bool isotpset = _emailServices.verifyOtp(email, otp);
                if (!isotpset)
                {
                    var r = new ApiResponse<bool>(404, "wrong otp", isotpset);
                    return NotFound(r);
                }

                var res = new ApiResponse<bool>(200, "otp verified", isotpset);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
                return StatusCode(500, r);
            }
        }

        [HttpPatch("Reset-Password")]

        public async Task<IActionResult> ResetPassword(string email, string Newpassword)
        {
            try
            {
                string passwordPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

                if (!Regex.IsMatch(Newpassword, passwordPattern))
                {
                    return BadRequest(new { error = "Password does not meet complexity requirements." });
                }

                bool isPasswordset = await _vendorServices.ResetPassword(email, Newpassword);
                if (!isPasswordset)
                {
                    var r = new ApiResponse<bool>(404, "User not exict", isPasswordset);
                    return NotFound(r);
                }

                var res = new ApiResponse<bool>(200, "successfully reseted password", isPasswordset);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
                return StatusCode(500, r);
            }
        }

        [HttpPost]
        public async Task<IActionResult>VendorAssignDeliveryperson(Guid pickupid)
        {
            try
            {
                var venIdResult = GetUserIdFromClaims();
                var vendorerId = venIdResult.Value;
                var res = await _vendorServices.VendorAssignDeliveryPerson(vendorerId, pickupid);
                if(res)
                {
                    return Ok("Delivery person assigned succesfully");
                }
                return BadRequest("Error while Assigning delivery person");
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
                return StatusCode(500, r);
            }
        }

    }
}
