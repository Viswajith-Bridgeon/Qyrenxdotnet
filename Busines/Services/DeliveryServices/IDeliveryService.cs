



using Microsoft.AspNetCore.Http;
using Qyrenx.Business.Models.DTOs.Deliverypersons;

namespace Qyrenx.Business.Services.DeliveryServices
{
    public interface IDeliveryService
    {
        Task<bool> Register(DeliveryPersonRegDto regdto,IFormFile license);
        Task<DeliveryPersonLoginViewDto> Login(DeliveryPersonLoginDto logindto);
        Task<bool> verify(string mail);
        Task<IEnumerable<DeliveryPersonDto>>GetAllDeliveryPeresons();
        Task<DeliveryPersonDto> GetDeliveryPeresonById(Guid Id);
        Task<bool>BlockOrUnblock(Guid Id);
     
    }
}
