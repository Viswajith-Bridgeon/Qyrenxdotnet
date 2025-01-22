using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.StatusRepo
{
    public class StatusRepo:IstatusRepo
    {
        private readonly QyrenxContext _context;
        public StatusRepo(QyrenxContext context)
        {
            _context = context;
        }
        public async Task<List<Status>> GetAllStatus()
        {
            try
            {
                var data=await _context.Status.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
        public async Task<List<Status>> GetStatusByPickId(Guid id)
        {
            try
            {
                var data = await _context.Status.Where(p=>p.PickupId==id).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}
