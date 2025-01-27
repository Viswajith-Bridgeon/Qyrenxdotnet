using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.Pickuprep
{
    public interface IpickupsRepo
    {
        Task<List<Pickup>> GetAllPickup();
        Task<Pickup> GetPickupById(Guid id);

        Task<Pickup> GetPickupByGadId(Guid gadId);

        Task<List<Pickup>> GetPickupByVendorId(Guid vid);


        Task<List<Pickup>> GetPickupByDevilveryReturnId(Guid vid);

        Task<Vendor> GetVendorByPickupId(Guid id);
    }
}
