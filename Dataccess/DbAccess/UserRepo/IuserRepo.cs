using Qyrenx.Dataccess.ApiResponses;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.UserRepo
{
    public interface IuserRepo
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(Guid id);

        Task<List<User>> SearchUsers(string name);
        Task<string>BlockOrUnblock(Guid id);

        Task <User>GetUserByEmail(string email);

    }
}
