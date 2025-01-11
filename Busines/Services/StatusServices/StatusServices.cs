using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.DTOs.StatusDtos;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.StatusServices
{
    public class StatusServices :IStatusServices
    {

        private readonly QyrenxContext _context;
        private readonly IMapper _mapper;

        public StatusServices(QyrenxContext context, IMapper mapper) 
        {
            _context = context;     
            _mapper = mapper;
        }



        public async Task<ICollection<StatusViewDto>> GetStatuses(Guid pid)
        {
            try
            {
                var statuss=await _context.Status.Where(e=>e.PickupId==pid).ToListAsync();
                return _mapper.Map<ICollection<StatusViewDto>>(statuss);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }

        }

        public async Task<bool> AddStatus(Guid pid, string statuss)
        {
            try
            {
                var pick = await _context.Pickups.FirstOrDefaultAsync(p => p.Id == pid);
                if (pick == null)
                {
                    return false;
                }
                var stat = new Status 
                {
                    PickupId = pid,
                    Statuss = statuss
                };
                await _context.Status.AddAsync(stat);
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
