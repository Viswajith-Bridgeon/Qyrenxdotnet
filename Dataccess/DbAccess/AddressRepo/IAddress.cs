using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.AddressRepo
{
    public interface IAddress
    {
         Task<ICollection<Address>> GetAllAddress();
        Task<ICollection<Address>> GetAddressById(Guid id);
        Task<bool> AddAddress(Guid id,Address add);
        Task<bool> UpdateAddress(Guid id, Guid Aid,Address address);
        Task<bool> DeleteAddressById(Guid usid,Guid addid);

    }
}
