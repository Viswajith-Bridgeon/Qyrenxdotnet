
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.Models.Entities;
using Qyrenx.Business.Models.DTOs.UserDTO;
using Qyrenx.Dataccess.ApplicationDbContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Qyrenx.Dataccess.DbAccess;
using Qyrenx.Business.DTOs;
using Qyrenx.Business.Services.JwtServices;
using Qyrenx.Dataccess.DbAccess.UserRepo;
using Qyrenx.Business.DTOs.VendorDtos;
using Qyrenx.Dataccess.DbAccess.VendorCostRepo;
using Qyrenx.Dataccess.DbAccess.VendorRepo;


namespace Qyrenx.Business.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly QyrenxContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;
        private readonly IJwtService _jwtService;
        private readonly IuserRepo _userRepo;
        private readonly IVendorCostRepo _vendorCostRepo;
        private readonly IVendorRepo _vendorRepo;
        public UserServices(QyrenxContext mainDbContext, IMapper mapper, IEmailServices emailServices, IJwtService jwtService, IuserRepo userRepo, IVendorCostRepo vendorCostRepo, IVendorRepo vendorRepo)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _emailServices = emailServices;
            _jwtService = jwtService;
            _userRepo = userRepo;
            _vendorCostRepo = vendorCostRepo;
            _vendorRepo = vendorRepo;
        }

        public async Task<string> registration(UserDto user)
        {
            try
            {
                var data = await _userRepo.GetUsers();
                var isExist =data.FirstOrDefault(e => e.Email == user.Email);

                if (isExist != null)
                {
                    return "user already exict";
                }

                bool emailverify = _emailServices.verifyOtp(user.Email, user.otp);
                if (emailverify)
                {

                    var haspassword = BCrypt.Net.BCrypt.HashPassword(user.HashPassword);
                    user.HashPassword = haspassword;

                    var u = new User
                    {
                        Name = user.Name,
                        Email = user.Email,
                        HashPassword = haspassword,
                        Mobile = user.Mobile

                    };
                    u.CreatedBy = user.Name;                   
                    _mainDbContext.Users.Add(u);
                    await _mainDbContext.SaveChangesAsync();


                    return "succesfully registered";
                }
                return "wrong otp";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }




        public async Task<AllLoginresponses> login(string email, string password)
        {
            try
            {
                var p = await _mainDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
                if (p == null)
                {
                    return new AllLoginresponses { Error = "Not Found" };
                }
                bool pass = BCrypt.Net.BCrypt.Verify(password, p.HashPassword);
                if (!pass)
                {
                    return new AllLoginresponses { Error = "Invalid Password" };

                }
                if (p.IsBlock == true)
                {
                    return new AllLoginresponses { Error = "User Blocked" };
                }
                string token =_jwtService.GenerateJwt(p.Id,p.Email,p.Role);
                string RefreshToken = await _jwtService.CreaterefreshToken(p.Id, p.Email, p.Role);
                p.RefreshToken = RefreshToken;
                p.TokenExpiryTime = DateTime.UtcNow.AddDays(30);
                await _mainDbContext.SaveChangesAsync();
                return new AllLoginresponses
                {
                    Id = p.Id,
                    Name = p.Name,
                    Email = p.Email,
                    Role = p.Role,
                    Token = token,
                    refreshToken = RefreshToken
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

        }




        public async Task<List<UserViewDto>> GetUsers()
        {
            try
            {
                var data = await _userRepo.GetUsers();
                return _mapper.Map<List<UserViewDto>>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

        }

        public async Task<UserViewDto> GetUserById(Guid id)
        {
            try
            {
                var data = await _userRepo.GetUserById(id);
                if (data == null)
                {
                    return null;
                }
                return _mapper.Map<UserViewDto>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

        }




        public async Task<ApiResponse<string>> BlockOrUnblock(Guid id)
        {
            try
            {
                
                var us = await _userRepo.BlockOrUnblock(id);
                if (us == "user is not found")
                {
                    return new ApiResponse<string>(404, "user is not found");
                }
               if(us =="user is blocked")
                {
                    return new ApiResponse<string>(200,us);
                }
                return new ApiResponse<string>(200, us);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }





        public async Task<List<UserViewDto>> SearchUsers(string name)
        {
            try
            { 
                var users = _userRepo.SearchUsers(name);
                return _mapper.Map<List<UserViewDto>>(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }



        public async Task<bool> Updateuser(Guid id, UserUpdateDto dto)
        {
            try
            {
                
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                {
                    return false;
                }
                user.Name = dto.Name;
                user.Mobile = dto.Mobile;
                user.UpdatedBy=user.Name;
                user.UpdatedOn=DateTime.UtcNow;
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }



        public async Task<bool> ResetPassword(string Email, string password)
        {
            try
            {
                var user = await _userRepo.GetUserByEmail(Email);
                if (user == null)
                {
                    return false;
                }
                var haspassword = BCrypt.Net.BCrypt.HashPassword(password);
                user.HashPassword = haspassword;
                user.UpdatedOn = DateTime.UtcNow;
                user.UpdatedBy = user.Name;
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }




        public async Task<bool> DeleteUser(Guid id)
        {
            try
            {
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                {
                    return false;
                }
                user.UpdatedOn = DateTime.UtcNow;
                user.UpdatedBy=user.Name;
                user.IsDelete = true;
                user.DeletedBy = user.Name;
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

      


    


    }
}
