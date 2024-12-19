using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qyrenx.ApiResponses;
using Qyrenx.Models.DTOs.Deliverypersons;
using Qyrenx.Models.Entities;
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
                return Ok(new ApiResponse <string> (200,"success!"));
            }
            return BadRequest(new ApiResponse<string>(400,"failed"));
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
        [HttpPatch("verify")]
        public async Task<IActionResult> Verify(string mail)
        {
            if (!string.IsNullOrEmpty(mail))
            {
                var deliveryverification = await _deliveryService.verify(mail);
                if (deliveryverification)
                {
                    return Ok(new ApiResponse<string>(200, "success!"));
                }
                return BadRequest(new ApiResponse<string>(404, "enter valid email"));

            }
            return BadRequest(new ApiResponse<string>(400,"enter email address"));
        }
        [HttpGet("getallDelliverypersons")]
        public async Task<ActionResult<IEnumerable<DeliveryPersonDto>>> GetAllDeliveryPeresons()
        {
            var persns=await _deliveryService.GetAllDeliveryPeresons();
            if(persns == null)
            {
                return BadRequest(Enumerable.Empty<DeliveryPersonDto>());
            }
            return Ok(persns);
        }
        [HttpGet("getDeliverpersonById{id}")]
        public async Task<ActionResult<DeliveryPersonDto>> GetDeliveryPeresonById(Guid id)
        {
            var persn=await _deliveryService.GetDeliveryPeresonById(id);
            if(persn == null)
            {
                return NotFound(new ApiResponse<string>(404,"failed"));
            }   
            return Ok(persn);
        }
        [HttpPut("deliverypersonblockandUnblock{id}")]
        public async Task<IActionResult> BlockOrUnblock(Guid id)
        {
            var blocstatus=await _deliveryService.BlockOrUnblock(id);
            if (blocstatus == null)
            {
                return BadRequest(new ApiResponse<string>(400, "failed"));
            }
            return Ok(new ApiResponse<string>(200,"success!"));
        }
        [HttpPost("send otp")]
        public async Task<IActionResult> SendOtp(string mail)
        {
            var otp = _deliveryService.SendOtp(mail);
            if (otp == null)
            {
                return BadRequest(new ApiResponse<string>(404, "success!"));
            }
            return Ok(new ApiResponse<string>(200, "success!"));
        }
        [HttpPost("verify otp")]
        public async Task<IActionResult> Verify(string mail, string otp)
        {
            var verification = await _deliveryService.VerifyOtp(mail, otp);
            if (verification)
            {
                return Ok(new ApiResponse<string>(200, "success"));
            }
            return BadRequest(new ApiResponse<string>(404,"error in verification"));
        }
    }
}
