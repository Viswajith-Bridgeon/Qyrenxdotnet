using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.GadgetRepo
{
    public class GadgetRepo:IgadgetRepo
    {
        private readonly QyrenxContext _context;
        public GadgetRepo(QyrenxContext qyrenxContext)
        {
            _context = qyrenxContext;
        }
        public async Task<bool> Addgadget(Guid id, Gadget dto, User user, string gadgetimg)
        {
            try
            {
                var g = new Gadget
                {
                    UserId = id,
                    CategoryId = dto.CategoryId,
                    GadgetName = dto.GadgetName,
                    Image = gadgetimg,
                    Description = dto.Description,
                    AddressId = dto.AddressId,
                };
                g.CreatedOn = DateTime.Now;
                g.CreatedBy = user.Name;
                await _context.Gadgets.AddAsync(g);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
        public async Task<List<Gadget>> Getgadgets()
        {
            try
            {
                var gad=await _context.Gadgets.ToListAsync();
                return gad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
       public async Task<Gadget> GetordergadgetsById(Guid id)
        {
            try
            {
                var data=await _context.Gadgets.FirstOrDefaultAsync(x => x.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }




        public async Task<List<Gadget>> GetgadgetsByUserId(Guid id)
        {
            try
            {
                var data = await _context.Gadgets.Where(x => x.UserId == id).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
