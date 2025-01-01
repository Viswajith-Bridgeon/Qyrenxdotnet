using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.DTOs.Deliverypersons;
using Qyrenx.Business.Models.DTOs.Deliverypersons;
using Qyrenx.Business.Services.DeliveryServices;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.Models.Entities;
namespace Qyrenx.present.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private  readonly IDeliveryService _deliveryService;
        private readonly IEmailServices _emailServices;
        public DeliveryController(IDeliveryService deliveryService,IEmailServices emailServices)
        {
            _deliveryService = deliveryService;
            _emailServices = emailServices;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]DeliveryPersonRegDto regDto,IFormFile licence)
        {
            var res = await _deliveryService.Register(regDto,licence);
            if (res)
            {
                return Ok(new ApiResponse <string> (200,"success!"));
            }
            return BadRequest(new ApiResponse<string>(400,"failed"));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] DeliveryPersonLoginDto loginViewDto)
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
            var otp = _emailServices.sendOtp(mail);
            if (otp == null)
            {
                return BadRequest(new ApiResponse<string>(404, "success!"));
            }
            return Ok(new ApiResponse<string>(200, "success!"));
        }
        [HttpPost("verify otp")]
        public async Task<IActionResult> Verify(string mail, string otp)
        {
            var verification =  _emailServices.verifyOtp(mail, otp);
            if (verification)
            {
                return Ok(new ApiResponse<string>(200, "success"));
            }
            return BadRequest(new ApiResponse<string>(404,"error in verification"));
        }


        [HttpGet("getalldeliverypersononline")]
        public async Task<ActionResult<List<DeliveryPersonOnlineDto>>> GetDeliveryPersonsOnline()
        {
            var data= await _deliveryService.GetAllDeliveryPersonOnline();
            if (data == null)
            {
                return BadRequest(new ApiResponse<string>(404,"notfound"));
            }
            return Ok(new ApiResponse<List<DeliveryPersonOnlineDto>>(200,"success",data,null));
        }

        [HttpGet("getDeliverpersonActivity")]
        public async Task<ActionResult<DeliveryPersonOnline>>GetDeliveryPersonActivity(Guid id,decimal lat,decimal lon)
        {
            var data=await _deliveryService.DeliveryPersonActivity(id,lat,lon); 
            if(data == null)
            {
                return BadRequest(new ApiResponse<string>(404, "failed!"));
            }
            return Ok(new ApiResponse<DeliveryPersonOnline>(200,"success",data,null));
        }


    }
}
