using AutoMapper;
using Qyrenx.Models.DTOs.UserDTO;
using Qyrenx.Models.Entities;

namespace Qyrenx.Mapper
{
    public class AutoMapping:Profile
    {
      public AutoMapping()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserViewDto, User>().ReverseMap();

        }


    }
}
