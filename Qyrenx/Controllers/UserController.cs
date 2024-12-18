using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Qyrenx.ApiResponses;
using Qyrenx.Models.DTOs.UserDTO;
using Qyrenx.Models.Entities;
using Qyrenx.Services.EmailServices;
using Qyrenx.Services.UserServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Qyrenx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IEmailServices _emailServices;
        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;


        public UserController( IConfiguration configuration,IUserServices userServices, IEmailServices emailServices )
        {
         
            _configuration = configuration;
            _userServices = userServices;
            _emailServices = emailServices;
        }


        [HttpPost("SendOtp")]

        public async Task<IActionResult> sendotp(string email)
        {
            try
            {
               
                bool isotpset = await _emailServices.sendOtp(email);
                var res = new ApiResponse<bool>(200, "otp sending", isotpset);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error",null, ex.Message);
                return StatusCode(500, r);
            }
        }



        [HttpPost("signup")]
        public async Task<IActionResult> signup([FromForm] UserDto user)
        {
            try
            {
                var res = await _userServices.registration(user);
                if (res== "user already exict")
                {
                    var r = new ApiResponse<string>(409, "user already exict");
                    return Conflict(r);
                }
                if (res== "wrong otp")
                {
                    var r = new ApiResponse<string>(400,"wrong otp");
                    return BadRequest(r);
                }

                return Ok(new ApiResponse<string>(200, res));
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }
        }
        [HttpPost("login")]


        public async Task<IActionResult> login(string email, string password)
        {
            try
            {
                var user = await _userServices.login(email, password);
                if (user.IsBlock)
                {
                    var res = new ApiResponse<string>(409, "user is block");
                    return StatusCode(res.StatusCode, res);
                }

                if (user.Name == null)
                {
                    var res = new ApiResponse<string>(404, "invalid email or password");
                    return StatusCode(res.StatusCode, res);
                }
                return Ok(new ApiResponse<object>(200,"successfully login", new { user.Name, user.Email, user.Role }) );


            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }
        }



        [HttpGet("GetAllUsers")]

        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userServices.GetUsers();
                return Ok(new ApiResponse<List<UserViewDto>>(200, "fetched all users", users));
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }
        }
        [HttpGet("GetUserById{id}")]


        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userServices.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new ApiResponse<string>(404, "user not found", null));
                }
                var res = new ApiResponse<UserViewDto>(200, "user fetched by id", user);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("BlockOrUnblock{id}")]
        public async Task<IActionResult> BlockOrUnblock(Guid id)
        {
            try
            {
                var res = await _userServices.BlockOrUnblock(id);
                return StatusCode(res.StatusCode, res);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }
        }

        [HttpGet("SearchByName{name}")]

        public async Task<IActionResult> SearchUsers(string name)
        {
            try
            {
                var users = await _userServices.SearchUsers(name);
                return Ok(new ApiResponse<List<UserViewDto>>(200, "Fetched user by name", users));
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "sewrver error", null, ex.Message);
                return StatusCode(500, r);
            }

        }
















       
    }
}
