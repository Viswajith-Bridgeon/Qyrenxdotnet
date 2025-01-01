
using Microsoft.AspNetCore.Mvc;
using Qyrenx.Business.Models.DTOs.UserDTO;
using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.Models.Entities;


namespace Qyrenx.Business.Services.UserServices
{
    public interface IUserServices
    {
        Task<string> registration([FromForm] UserDto user);


        Task<User> login(string email, string password);


        Task<List<UserViewDto>> GetUsers();
        Task<UserViewDto> GetUserById(Guid id);

        Task<List<UserViewDto>> SearchUsers(string name);

        Task<ApiResponse<string>> BlockOrUnblock(Guid id);

        Task<bool> Updateuser(Guid id, UserUpdateDto user);

        Task<bool>  ResetPassword(string Email, string password);
        //Task<bool>SelectDeliveryPerson(Guid id);
    }
}
