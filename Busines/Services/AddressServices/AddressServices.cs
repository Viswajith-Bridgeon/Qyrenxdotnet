using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.DTOs.AddressDtos;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.AddressServices
{
    public class AddressServices : IAddressServices
    {
        private readonly QyrenxContext _mainDbContext;
        private readonly IMapper _mapper;
        public AddressServices(QyrenxContext mainDbContext, IMapper mapper, IEmailServices emailServices)
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
        }


        public async Task<bool> addAddress(Guid Id, AddressAddDto Dto)
        {
            try
            {
                var user = await _mainDbContext.Users.FindAsync(Id);
                if (user == null)
                {
                    return false;
                }
                var address = _mapper.Map<Address>(Dto);
                address.Role=user.Role;
                address.UserId = user.Id;
                await _mainDbContext.AddAsync(address);
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }




        public async Task<List<AddressViewDto>> ViewAddress(Guid Id)
        {
            try
            {
                var address=await _mainDbContext.Address.Where(x => x.UserId == Id).ToListAsync();
                return _mapper.Map<List<AddressViewDto>>(address);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }


        public async Task<bool> UpdateAddrsss(Guid AddressId, AddressAddDto dto)
        {
            try
            {
                var address =await _mainDbContext.Address.FirstOrDefaultAsync(x => x.Id == AddressId);
                if(address == null)
                {
                    return false;
                }
                var user =await _mainDbContext.Users.FirstOrDefaultAsync(p=>p.Id==address.UserId);
                address.City = dto.City;
                address.House = dto.House;
                address.LandMark = dto.LandMark;
                address.PostalCode = dto.PostalCode;
                address.UpdatedOn=DateTime.Now;
                address.UpdatedBy = user.Name;
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }


        public async Task<bool> DeleteAddrsss(Guid AddressId)
        {
            try
            {
                var address = await _mainDbContext.Address.FirstOrDefaultAsync(x => x.Id == AddressId);
                if (address == null)
                {
                    return false;
                }
                var user = await _mainDbContext.Users.FirstOrDefaultAsync(p => p.Id == address.UserId);
                address.UpdatedOn = DateTime.Now;
                address.UpdatedBy = user.Name;
                address.IsDelete = true;
                address.DeletedBy = user.Name;
                await _mainDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<AddressViewDto> GetAddrsssById(Guid Aid)
        {
            try
            {
                var addresss = await _mainDbContext.Address.FindAsync(Aid);
                if(addresss==null)
                {
                    return new AddressViewDto();
                }
                return _mapper.Map<AddressViewDto>(addresss);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
