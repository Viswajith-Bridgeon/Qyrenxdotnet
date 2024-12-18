using AutoMapper;
using Qyrenx.Models.DTOs.Deliverypersons;
using Qyrenx.Models.Entities;

namespace Qyrenx.Mapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<DeliveryPerson, DeliveryPersonRegDto>().ReverseMap();

        }
    }
}
