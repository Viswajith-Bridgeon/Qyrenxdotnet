using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Qyrenx.Dataccess.DbAccess.DeliveryRepo
{
    public class DeliveryServiceRepo:IdeliveryRepo
    {
        private readonly QyrenxContext _context;
        public DeliveryServiceRepo(QyrenxContext context)
        {
            _context = context;
        }
        public async Task<string>Register(DeliveryPerson person,string Icloud_file)
        {
            try
            {
                var exist = await _context.DeliveryPersons.FirstOrDefaultAsync(p => p.Email == person.Email);
                if (exist != null)
                {
                    return "person with email already exists";
                }

                  var mapdata = new DeliveryPerson
                    {
                        Name = person.Name,
                        Email = person.Email,
                        DrivingLicense = Icloud_file,
                        HashPassword = BCrypt.Net.BCrypt.HashPassword(person.HashPassword),
                        Mobile = person.Mobile
                    };
                    _context.DeliveryPersons.Add(mapdata);
                    await _context.SaveChangesAsync();
                    return "success!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<IEnumerable<DeliveryPerson>> GetAllDeliveryPeresons()
        {
            try 
            {
                var data = await _context.DeliveryPersons.ToListAsync();
                if (data != null) 
                {
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<DeliveryPerson> GetDeliveryPeresonById(Guid Id)
        {
            try
            {
                var data=await _context.DeliveryPersons.FirstOrDefaultAsync(x => x.Id == Id);
                if(data != null)
                {
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<bool> BlockOrUnblock(Guid id)
        {
           
            var exist = await _context.DeliveryPersons.FirstOrDefaultAsync(x => x.Id == id);
            if (exist != null)
            {
                exist.IsBlock = !exist.IsBlock;
                _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> verify(Guid id)
        {
            try
            {
                var vendor = await _context.DeliveryPersons.FindAsync(id);
                if (vendor == null)
                {
                    return false;
                }
                vendor.IsVerified = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an ERORR in VERIFICATION");
            }
        }

        public async Task<List<DeliveryPersonOnline>> GetAllDeliveryPersonOnline()
        {
            try
            {
                var user = await _context.DeliveryPersonOnlines.ToListAsync();
                
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<List<DeliveryPersonOnline>> GetActiveDeliveryPersons()
        {
            var user =await _context.DeliveryPersonOnlines.Where(p => p.IsActive == true).ToListAsync();
            if (user != null)
            {
                return user;
            }
            return new List<DeliveryPersonOnline>();
        }

      
    }
}
