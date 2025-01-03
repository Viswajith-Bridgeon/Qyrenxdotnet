using Qyrenx.Business.DTOs.AddressDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.AddressServices
{
    public interface IAddressServices
    {


        Task<bool>  addAddress(Guid Id,AddressAddDto Dto);

        Task<List<AddressViewDto>> ViewAddress(Guid Id);

        Task<bool> UpdateAddrsss(Guid Id,AddressAddDto dto);

        Task<bool> DeleteAddrsss(Guid id);
        Task<AddressViewDto> GetAddrsssById(Guid Aid);
    }
}
