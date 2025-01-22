using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.Pickuprep
{
    public class PickupsRepo: IpickupsRepo
    {
        private readonly QyrenxContext _context;
        public PickupsRepo(QyrenxContext context)
        {
            _context = context;
        }
        public async Task<List<Pickup>> GetAllPickup()
        {
            try
            {
                var data=await _context.Pickups.Include(p => p.Gadget).ThenInclude(v=>v.Users).Include(v=>v.Vendors).ThenInclude(a=>a.VendorAddress).Include(e => e.DeliveryPersons).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<Pickup> GetPickupById(Guid id)
        {
            try
            {
                var data=await _context.Pickups.FirstOrDefaultAsync(p => p.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }


        public async Task<Pickup> GetPickupByGadId(Guid gadId)
        {
            try
            {
                var data = await _context.Pickups.FirstOrDefaultAsync(p => p.GadgetId == gadId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }


        public async Task<List<Pickup>> GetPickupByVendorId(Guid vid)
        {
            try
            {
                var data = await _context.Pickups.Where(p => p.VendorId==vid).Include(p => p.Gadget).ThenInclude(v => v.Users).Include(v => v.Vendors).ThenInclude(a => a.VendorAddress).Include(e => e.DeliveryPersons).ToListAsync(); ;
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

    }
}
