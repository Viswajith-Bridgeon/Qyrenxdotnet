using Qyrenx.Models.DTOs.Deliverypersons;

namespace Qyrenx.Services.DeliveryServices
{
    public interface IDeliveryService
    {
        Task<bool> Register(DeliveryPersonRegDto regdto);
        Task<DeliveryPersonLoginViewDto> Login(DeliveryPersonLoginDto logindto);
        //Task<bool> verify(string mail);

    }
}
