
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


namespace Qyrenx.Business.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly QyrenxContext _mainDbContext;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;
        private readonly IDbAccess _dbAccess;
        public UserServices(QyrenxContext mainDbContext, IMapper mapper, IEmailServices emailServices, IDbAccess dbAccess)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _emailServices = emailServices;
            _dbAccess = dbAccess;
        }

        public async Task<string> registration(UserDto user)
        {
            try
            {
                var data = await _dbAccess.GetAllUsers();
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




        public async Task<User> login(string email, string password)
        {
            try
            {
                var p = await _mainDbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
                if (p == null)
                {
                    return new User();
                }
                bool pass = BCrypt.Net.BCrypt.Verify(password, p.HashPassword);
                if (!pass)
                {
                    return new User();

                }
                return p;
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
                var data = await _dbAccess.GetAllUsers();
                var u=data.Where(e => e.Role != "Admin").ToList();
                return _mapper.Map<List<UserViewDto>>(u);
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
                var data = await _dbAccess.GetAllUsers();
                   var user =  data.FirstOrDefault(p=>p.Id==id);
                if (user == null)
                {
                    return null;
                }

                return _mapper.Map<UserViewDto>(user);


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
                var data = await _dbAccess.GetAllUsers();
                var us = data.FirstOrDefault(p=> p.Id==id);
                if (us == null)
                {
                    return new ApiResponse<string>(404, "user is not found");
                }
                if (us.IsBlock)
                {
                    us.IsBlock = false;
                    await _mainDbContext.SaveChangesAsync();
                    return new ApiResponse<string>(200, "user is blocked");
                }
                else
                {
                    us.IsBlock = true;
                    await _mainDbContext.SaveChangesAsync();
                    return new ApiResponse<string>(200, "user is unblocked");
                }
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
                var data = await _dbAccess.GetAllUsers();
                var users= data.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
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
                var data = await _dbAccess.GetAllUsers();
                var user =data.FirstOrDefault(p => p.Id == id);
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
                var data = await _dbAccess.GetAllUsers();
                var user = data.FirstOrDefault(u => u.Email == Email);
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
                var data = await _dbAccess.GetAllUsers();
                var user = data.FirstOrDefault(u => u.Id==id);
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
