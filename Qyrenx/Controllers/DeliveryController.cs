using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Models.DTOs.Deliverypersons;
using Qyrenx.Services.DeliveryServices;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private  readonly IDeliveryService _deliveryService;
        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(DeliveryPersonRegDto regDto)
        {
            var res = await _deliveryService.Register(regDto);
            if (res)
            {
                return Ok("you registered successfully");
            }
            return BadRequest("registration failed");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(DeliveryPersonLoginDto loginViewDto)
        {
            if (loginViewDto == null)
            {
                return BadRequest("enter credentials");
            }
            var deliveryperson =await  _deliveryService.Login(loginViewDto);
            if (deliveryperson != null)
            {
                return Ok(deliveryperson);
            }
            return NoContent();
        }

    }
}
