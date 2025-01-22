using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.VendorCostRepo
{
    public class VendorCostServicRepo : IVendorCostRepo
    {
        private readonly QyrenxContext _context;
        public VendorCostServicRepo(QyrenxContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<VendorCost>> GetAllVendorCost()
        {
            try
            {
                var ven_cost = await _context.VendorCost.ToListAsync();
                if(ven_cost == null) 
                    return Enumerable.Empty<VendorCost>();
                return ven_cost;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetCoordinatesFromAddress: {ex.Message}", ex);
            }

        }

        public async Task<VendorCost> GetVendorCostByPickup(Guid pickid)
        {

            try
            {
                var ven_cost = await _context.VendorCost.FirstOrDefaultAsync(vc=>vc.PickupId == pickid);
                if (ven_cost == null)
                    return new VendorCost();
                return ven_cost;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetCoordinatesFromAddress: {ex.Message}", ex);
            }
        }




        public async Task<VendorCost> GetVendorCostById(Guid vc_id)
        {
            try
            {
                var ven_cost = await _context.VendorCost.FirstOrDefaultAsync(vc => vc.Id==vc_id);
                if (ven_cost == null)
                    return new VendorCost();
                return ven_cost;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetCoordinatesFromAddress: {ex.Message}", ex);
            }
        }
    }
}
