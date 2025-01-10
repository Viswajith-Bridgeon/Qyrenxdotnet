using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult>VerifyPickups(Guid PiickId)
        {
            try
            {
                var userIdResult = GetUserIdFromClaims();
                var userId = userIdResult.Value;
                var verify = await _pickupServices.VerifyPickup(PiickId,userId);
                if (verify)
                {
                    var resp=new ApiResponse<bool>(200, "verified", verify);
                    return Ok(resp);
                }
                return BadRequest();
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







        private ActionResult<Guid> GetUserIdFromClaims()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdString, out Guid userId))
            {
                return userId;
            }

            return Unauthorized();
        }
    }
}
