


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Qyrenx.Business.Models.DTOs.UserDTO;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Business.Services.JwtServices;
using Qyrenx.Business.Services.UserServices;
using Qyrenx.Dataccess.ApiResponses;
using System.Text.RegularExpressions;

namespace Qyrenx.present.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IEmailServices _emailServices;
        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;
        public readonly IJwtService _jwtService;


        public UserController( IConfiguration configuration,IUserServices userServices, IEmailServices emailServices , IJwtService jwtService )
        {
         
            _configuration = configuration;
            _userServices = userServices;
            _emailServices = emailServices;
            _jwtService = jwtService;
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
        public async Task<IActionResult> signup( UserDto user)
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
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
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
                string token =  _jwtService.GenerateJwt(user.Id, user.Email, user.Role);
                return Ok(new ApiResponse<object>(200,"successfully login", new { user.Name, user.Email, user.Role ,token}) );


            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
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
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
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
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
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
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
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
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
                return StatusCode(500, r);
            }

        }

        [HttpPut("UpdateUser{id}")]

        public async Task<IActionResult> UpdateUser(Guid id, [FromForm] UserUpdateDto user)
        {
            try
            {
                bool res = await _userServices.Updateuser(id, user);
                if (!res)
                {
                    var re = new ApiResponse<bool>(400, "Invalid userId", res);
                    return StatusCode(400, re);
                }
                var r = new ApiResponse<bool>(200, "successfully updated", res);
                return Ok(r);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
                return StatusCode(500, r);
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

        public async Task<IActionResult> ResetPasswordVerify(string email,string otp)
        {
            try
            {

                bool isotpset =  _emailServices.verifyOtp(email, otp);
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

                bool isPasswordset = await _userServices.ResetPassword(email, Newpassword);
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

        [Authorize(Roles ="User")]
        [HttpPatch("deleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                var usedId = Guid.Parse(HttpContext.Items["Id"].ToString());
                bool user =await _userServices.DeleteUser(usedId);
                if (!user)
                {
                    var r = new ApiResponse<bool>(404, "error occured", user);
                    return NotFound(r);
                }

                var res = new ApiResponse<bool>(200, "succesfully deleted",user);
                return Ok(res);
            }
            catch (Exception ex)
            {
                var r = new ApiResponse<string>(500, "server error", null, ex.Message);
                return StatusCode(500, r);
            }
        }

    }
}
