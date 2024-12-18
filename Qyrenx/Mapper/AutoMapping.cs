using AutoMapper;
using Qyrenx.Models.Entities;
using Qyrenx.Models.DTOs.Deliverypersons;
using Qyrenx.Models.DTOs.VendorDtos;
using Qyrenx.Models.DTOs.UserDTO;
namespace Qyrenx.Mapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<DeliveryPerson, DeliveryPersonRegDto>().ReverseMap();
            CreateMap<Vendor, VendorRegisterDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserViewDto, User>().ReverseMap();
        }
    }
}
