using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs.VendorDtos;
using Qyrenx.Business.Services.PickupServices;
using Qyrenx.Business.Services.VendorServices;
using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.DbAccess.Pickuprep;
using Qyrenx.Dataccess.Models.Entities;
using System.Security.Claims;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupController : ControllerBase
    {
        private readonly IPickupServices _pickupServices;
        public PickupController(IPickupServices pickup)
        {
            _pickupServices = pickup;
        }

        [Authorize(Roles = "DeliveryPerson")]
        [HttpGet("ViewPickup")]
        public async Task<IActionResult> ViewPickupsDelPerson()
        {
            try
            {
                var userIdResult = GetUserIdFromClaims();
                var userId = userIdResult.Value;
                var res = await _pickupServices.GetPickupsDeliveryBoys(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }

        }
        [Authorize(Roles = "DeliveryPerson")]
        [HttpPost("verificationdelivery&user")]
        public async Task<IActionResult> VerifyPickups(Guid PiickId)
        {
            try
            {
                var userIdResult = GetUserIdFromClaims();
                var userId = userIdResult.Value;
                var verify = await _pickupServices.VerifyPickup(PiickId, userId);
                if (verify== "already verified")
                {
                    var resp = new ApiResponse<string>(409, verify);
                    return Conflict(resp);
                }
                if (verify == "not is delveryboy")
                {
                    var resp = new ApiResponse<string>(400, verify);
                    return BadRequest(resp);
                }
                if (verify == "something wrong in email")
                {
                    var resp = new ApiResponse<string>(400, verify);
                    return BadRequest(resp);
                }
                var res = new ApiResponse<string>(200, verify);
                return Ok(res);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
        }
        [Authorize(Roles = "DeliveryPerson")]
        [HttpGet("latlongofUser")]
        public async Task<IActionResult> LatLongOfUser(Guid PiickId)
        {
            try
            {
                var data = await _pickupServices.LatLongOfUser(PiickId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }

        }
        [Authorize(Roles = "Vendor")]
        [HttpPost("sendformtouser")]
        public async Task<IActionResult>SendFormToUser([FromBody]VendorCostDto details)
        {
            try
            {
                var userIdResult = GetUserIdFromClaims();
                var userId = userIdResult.Value;
                var res = await _pickupServices.SendFormToUser(userId, details);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }

        }


        [Authorize(Roles = "DeliveryPerson")]
        [HttpPost("DeliveryPersonVerficationOtp")]
        public async Task<IActionResult> DeliveryPersonOtpVerify(Guid PiickId, string otp)
        {
            try
            {
                var data = await _pickupServices.pickupVerificationofUser(PiickId, otp);
                if(data== "invalid pickup id")
                {
                    var res = new ApiResponse<string>(400, data);
                    return BadRequest(res);
                }
                if (data == "invalid user email")
                {
                    var res = new ApiResponse<string>(400, data);
                    return BadRequest(res);
                }
                var resp = new ApiResponse<string>(200, data);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }
          

        }

        [Authorize(Roles = "Vendor")]
        [HttpGet("ViewPickupOfVendor")]
        public async Task<IActionResult> ViewPickupsVendor()
        {
            try
            {
                var userIdResult = GetUserIdFromClaims();
                var userId = userIdResult.Value;
                var res = await _pickupServices.GetPickupsVendor(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }

        }



        [Authorize(Roles = "DeliveryPerson")]
        [HttpPost("verificationdelivery&Vendor")]
        public async Task<IActionResult> VerifyPickupsByDeliveryToVendor(Guid PiickId)
        {
            try
            {
                var userIdResult = GetUserIdFromClaims();
                var userId = userIdResult.Value;
                var verify = await _pickupServices.VerifyPickupByDeliveryboyToVendor(PiickId, userId);
                if (verify == "already verified")
                {
                    var resp = new ApiResponse<string>(409, verify);
                    return Conflict(resp);
                }
                if (verify == "invalid vendor email")
                {
                    var resp = new ApiResponse<string>(400, verify);
                    return BadRequest(resp);
                }
                var res = new ApiResponse<string>(200, verify);
                return Ok(res);

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



        [Authorize(Roles = "DeliveryPerson")]
        [HttpPost("VendorVerficationOtp")]
        public async Task<IActionResult> VendorOtpVerify(Guid PiickId, string otp)
        {
            try
            {
                var data = await _pickupServices.pickupVerificationofVendor(PiickId, otp);
                if(data)
                {
                    var res = new ApiResponse<bool>(200,"successfully completed otp verification" ,data);
                    return Ok(res);
                }
                var resp = new ApiResponse<bool>(400, "wrong otp", data);
                return BadRequest(resp);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException.Message);
            }

        }

    }
    }
