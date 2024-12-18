using Microsoft.AspNetCore.Mvc;
using Qyrenx.ApiResponses;
using Qyrenx.Models.DTOs.UserDTO;
using Qyrenx.Models.Entities;

namespace Qyrenx.Services.UserServices
{
    public interface IUserServices
    {
        Task<string> registration([FromForm] UserDto user);


        Task<User> login(string email, string password);


        Task<List<UserViewDto>> GetUsers();
        Task<UserViewDto> GetUserById(Guid id);

        Task<List<UserViewDto>> SearchUsers(string name);

        Task<ApiResponse<string>> BlockOrUnblock(Guid id);

    }
}
