using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.DTOs.Deliverypersons;
using Qyrenx.Business.Models.DTOs.Deliverypersons;
using Qyrenx.Business.Services.DeliveryServices;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Business.Services.JwtServices;
using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
namespace Qyrenx.present.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private  readonly IDeliveryService _deliveryService;
        private readonly IEmailServices _emailServices;
        private readonly QyrenxContext _context;
        private readonly IJwtService _jwtService;
        public DeliveryController(IDeliveryService deliveryService,IEmailServices emailServices,IJwtService jwtService, QyrenxContext qyrenxContext)
        {
            _deliveryService = deliveryService;
            _emailServices = emailServices;
            _jwtService = jwtService;
            _context = qyrenxContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]DeliveryPersonRegDto regDto,IFormFile licence)
        {
            var res = await _deliveryService.Register(regDto,licence);
            if (res== "success")
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
        [Authorize(Roles="Admin")]
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

        [HttpPost("getDeliverpersonActivity")]
        public async Task<ActionResult<DeliveryPersonOnline>>GetDeliveryPersonActivity(decimal lat,decimal lon)
        {
            var id = Guid.Parse(HttpContext.Items["Id"].ToString());

            var data =await _deliveryService.DeliveryPersonActivity(id,lat,lon); 
            if(data == null)
            {
                return BadRequest(new ApiResponse<string>(404, "failed!"));
            }
            return Ok(new ApiResponse<DeliveryPersonOnline>(200,"success",data,null));
        }



        [HttpPost("refreshOfDeliveryPerson")]
        public async Task<IActionResult> AccessTokenRefresh(string Refresh)
        {
            try
            {
                var user = await _context.DeliveryPersons.FirstOrDefaultAsync(e => e.RefreshToken == Refresh);
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
    }
}
