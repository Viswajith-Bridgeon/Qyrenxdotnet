using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Business.DTOs.AddressDtos;
using Qyrenx.Business.Services.EmailServices;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.DbAccess;
using Qyrenx.Dataccess.DbAccess.AddressRepo;
using Qyrenx.Dataccess.DbAccess.UserRepo;
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
        private readonly IAddress _Addres;
        private readonly IuserRepo _userRepo;
        public AddressServices(QyrenxContext mainDbContext, IMapper mapper, IEmailServices emailService,IAddress address,IuserRepo repo)    
        {
            _mainDbContext = mainDbContext;
            _mapper = mapper;
            _Addres = address;
            _userRepo = repo;   
        }


        public async Task<bool> addAddress(Guid Id, AddressAddDto Dto)
        {
            try
            {
                var user = await _userRepo.GetUserById(Id);
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
                var address = await _Addres.GetAddressById(Id);
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
                var address =await _Addres.GetAddressById(AddressId);
                if(address == null)
                {
                    return false;
                }
                var dat = _mapper.Map<Address>(dto);
                var value= await _Addres.UpdateAddress(AddressId,dat);
                if (value == true)
                {
                    return true;
                }
                return false;
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
                var address =await _Addres.GetAddressById(AddressId);
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
                var addresss = await _Addres.GetAddressById(Aid);
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
