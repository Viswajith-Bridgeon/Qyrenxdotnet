


using Qyrenx.Dataccess.Models.DTOs.Deliverypersons;

namespace Qyrenx.Business.Services.DeliveryServices
{
    public interface IDeliveryService
    {
        Task<bool> Register(DeliveryPersonRegDto regdto);
        Task<DeliveryPersonLoginViewDto> Login(DeliveryPersonLoginDto logindto);
        Task<bool> verify(string mail);
        Task<IEnumerable<DeliveryPersonDto>>GetAllDeliveryPeresons();
        Task<DeliveryPersonDto> GetDeliveryPeresonById(Guid Id);
        Task<bool>BlockOrUnblock(Guid Id);
     
    }
}
