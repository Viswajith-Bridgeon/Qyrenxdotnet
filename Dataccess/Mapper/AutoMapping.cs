using AutoMapper;
using Qyrenx.Dataccess.Models.DTOs.Deliverypersons;
using Qyrenx.Dataccess.Models.DTOs.UserDTO;
using Qyrenx.Dataccess.Models.DTOs.VendorDtos;
using Qyrenx.Dataccess.Models.Entities;


namespace Qyrenx.Dataccess.Mapper
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
