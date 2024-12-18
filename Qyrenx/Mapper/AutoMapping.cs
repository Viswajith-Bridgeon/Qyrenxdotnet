using AutoMapper;
using Qyrenx.Models.DTOs.VendorDtos;
using Qyrenx.Models.Entities;

namespace Qyrenx.Mapper
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
			CreateMap<Vendor, VendorRegisterDto>().ReverseMap();

		}
	}
}
