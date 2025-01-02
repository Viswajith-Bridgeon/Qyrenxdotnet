using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.DTOs.GadgetDtos;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.GadgetServices
{
    public class GadgetServices:IGadgetSerives
    {
        private readonly QyrenxContext _qyrenxContext;
        private readonly IMapper _mapper;


        public GadgetServices(QyrenxContext qyrenxContext,IMapper mapper)
        {
          _qyrenxContext = qyrenxContext;
            _mapper = mapper;
        }

        public async Task<bool> Addgadget(Guid id, GadgetAddDto dto)
        {
            try
            {
                var user = await _qyrenxContext.Users.FirstOrDefaultAsync(e => e.Id == id);
                if (user == null)
                {
                    return false;
                }
                var g = new Gadget
                {
                    UserId = id,
                    CategoryId = dto.CategoryId,
                    GadgetName = dto.GadgetName,
                    Image = dto.Image,
                    Description = dto.Description,
                    AddressId = dto.AddressId,
                };
                _qyrenxContext.Gadgets.Add(g);
                await _qyrenxContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
