using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;

namespace Qyrenx.Dataccess.DbAccess.AddressRepo
{
    public class AddressService : IAddress
    {
        private readonly QyrenxContext _context;
        public AddressService(QyrenxContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAddress(Guid id, Address add)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return false;
                }
                add.Role = user.Role;
                add.UserId = user.Id;
                await _context.Address.AddAsync(add);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<bool> DeleteAddressById(Guid usid, Guid addid)
        {
            try
            {
                var address = await _context.Address.FirstOrDefaultAsync(e => e.UserId == usid && e.Id == addid);
                if (address == null)
                {
                    return false;
                }
                var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == address.UserId);
                address.IsDelete = true;
                address.DeletedBy = user.Name;
                address.UpdatedOn = DateTime.Now;
                address.UpdatedBy = user.Name;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            try
            {
                var address = await _context.Address.Where(x => x.UserId == id).FirstOrDefaultAsync();
                return address;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<ICollection<Address>> GetAllAddress()
        {
            try
            {
                var address = await _context.Address.ToListAsync();
                return address;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<bool> UpdateAddress(Guid Aid,Address addres)
        {

            try
            {
                var address = await _context.Address.FirstOrDefaultAsync(e=> e.Id==Aid);
                if (address == null)
                {
                    return false;
                }
                var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == address.UserId);
                address.City = addres.City;
                address.House = addres.House;
                address.LandMark = addres.LandMark;
                address.PostalCode = addres.PostalCode;
                address.UpdatedOn = DateTime.Now;
                address.UpdatedBy = user.Name;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
