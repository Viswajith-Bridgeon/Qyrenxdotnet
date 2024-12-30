
using AutoMapper;
using Qyrenx.Business.DTOs.CategoryDto;
using Qyrenx.Business.Models.DTOs.Deliverypersons;
using Qyrenx.Business.Models.DTOs.UserDTO;
using Qyrenx.Business.Models.DTOs.VendorDtos;
using Qyrenx.Dataccess.Models.Entities;


namespace Qyrenx.Business.Mapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<DeliveryPerson, DeliveryPersonRegDto>().ReverseMap();
            CreateMap<Vendor, VendorRegisterDto>().ReverseMap();
            CreateMap<Vendor,VendorAdminViewDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserViewDto, User>().ReverseMap();
            CreateMap<Category,CategoryAddDto>().ReverseMap();
        }
    }
}
