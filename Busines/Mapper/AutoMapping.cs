
using AutoMapper;
using Qyrenx.Business.DTOs.AddressDtos;
using Qyrenx.Business.DTOs.CategoryDto;
using Qyrenx.Business.DTOs.Deliverypersons;
using Qyrenx.Business.DTOs.GadgetDtos;
using Qyrenx.Business.DTOs.StatusDtos;
using Qyrenx.Business.DTOs.VendorActiveDto;
using Qyrenx.Business.DTOs.VendorDtos;
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
            CreateMap<AddressAddDto, Address>().ReverseMap();
            CreateMap<AddressViewDto, Address>().ReverseMap();     
            CreateMap<DeliveryPersonOnlineDto, DeliveryPersonOnline>().ReverseMap();
            CreateMap<GadgetAddDto, Gadget>().ReverseMap();   
            CreateMap<VendorActiveDto,VendorOnline>().ReverseMap();
            CreateMap<VendorRegisterDto,VendorAddress>().ReverseMap();
            CreateMap<VendorCategoryAddDto,VendorCategory>().ReverseMap();
            CreateMap<DeliveryPerson, DeliveryPersonDto>().ReverseMap();
            CreateMap <GadgetviewDto,Gadget>().ReverseMap();
            CreateMap<PickupDto, Pickup>().ReverseMap()
                .ForMember(e => e.UserAddressId, e => e.MapFrom(e => e.Gadget.AddressId))
                .ForMember(e => e.UserId, e => e.MapFrom(e => e.Gadget.UserId))
                 .ForMember(e => e.Image, e => e.MapFrom(e => e.Gadget.Image))
                 .ForMember(e => e.GadgetName, e => e.MapFrom(e => e.Gadget.GadgetName))
                  .ForMember(e => e.Description, e => e.MapFrom(e => e.Gadget.Description))
                  .ForMember(e => e.UserName, e => e.MapFrom(e => e.Gadget.Users.Name))
                   .ForMember(e => e.UserNumber, e => e.MapFrom(e => e.Gadget.Users.Mobile))
                    .ForMember(e => e.ShopName, e => e.MapFrom(e => e.Vendors.ShopeName))
                     .ForMember(e => e.ShopOwnerNamw, e => e.MapFrom(e => e.Vendors.Name))
                       .ForMember(e => e.shopAddressId, e => e.MapFrom(e => e.Vendors.VendorAddress.Id))
                                              .ForMember(e => e.ShopNumber, e => e.MapFrom(e => e.Vendors.Mobile));


            CreateMap<PickupVendorDto, Pickup>().ReverseMap()
                 .ForMember(e => e.Image, e => e.MapFrom(e => e.Gadget.Image))
                 .ForMember(e => e.GadgetName, e => e.MapFrom(e => e.Gadget.GadgetName))
                  .ForMember(e => e.Description, e => e.MapFrom(e => e.Gadget.Description))
                  .ForMember(e => e.UserName, e => e.MapFrom(e => e.Gadget.Users.Name))
                    .ForMember(e => e.DeliveryBoyName, e => e.MapFrom(e => e.DeliveryPersons.Name))
                      .ForMember(e => e.DeliveryBoyNumber, e => e.MapFrom(e => e.DeliveryPersons.Mobile));


            CreateMap<StatusViewDto, Status>().ReverseMap()
              .ForMember(e => e.date, p => p.MapFrom(s => s.CreatedOn));
                  .ForMember(e => e.Description, e => e.MapFrom(e => e.Gadget.Description));
            CreateMap<Category, CategoryViewDto>().ReverseMap();
            CreateMap<VendorCost,VendorCostDto>().ReverseMap();
            CreateMap<VendorCost,VendorCostView>().ReverseMap();
           


        }
    }
}
