using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.VendorCostRepo
{
    public interface IVendorCostRepo
    {
        Task<IEnumerable<VendorCost>> GetAllVendorCost();
        Task<VendorCost> GetVendorCostByPickup(Guid pickid);

        Task<VendorCost> GetVendorCostById(Guid vc_id);

    }
}
