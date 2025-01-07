



using Microsoft.AspNetCore.Http;
using Qyrenx.Business.DTOs;
using Qyrenx.Business.DTOs.AddressDtos;
using Qyrenx.Business.DTOs.Deliverypersons;
using Qyrenx.Business.Models.DTOs.Deliverypersons;
using Qyrenx.Dataccess.Models.Entities;

namespace Qyrenx.Business.Services.DeliveryServices
{
    public interface IDeliveryService
    {
        Task<bool> Register(DeliveryPersonRegDto regdto,IFormFile license);
        Task<AllLoginresponses> Login(DeliveryPersonLoginDto logindto);
        Task<bool> verify(string mail);
        Task<IEnumerable<DeliveryPersonDto>>GetAllDeliveryPeresons();
        Task<DeliveryPersonDto> GetDeliveryPeresonById(Guid Id);
        Task<bool>BlockOrUnblock(Guid Id);
        Task<DeliveryPersonOnline> DeliveryPersonActivity(Guid id,decimal latt, decimal lonn);
        Task<List<DeliveryPersonOnlineDto>> GetAllDeliveryPersonOnline();
        Task<List<DeliveryPersonOnlineDto>> GetActiveDeliveryPersons();
        Task<(decimal lat, decimal lon)> GetCoordinatesFromAddress(Address address);
        Task<Guid> GetNearestDeliveryPerson(Guid id);
        //Task AssignPickupToNearestDeliveryPerson(string userAddress, Guid pickupId);



    }
}
