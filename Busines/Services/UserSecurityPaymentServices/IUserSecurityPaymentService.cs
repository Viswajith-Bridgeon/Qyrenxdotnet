using Qyrenx.Business.DTOs.UserSecurityPaymentDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.Services.UserSecurityPay
{
    public interface IUserSecurityPaymentService
    {
        Task<string> RazorOrderCreate(long price);
        bool PaymentVerify(UserSecurityRazorDto razorDto);
        Task<bool> CreateOrder(Guid id, UserSecurityInputDto inputorderDto);
        Task<IEnumerable<UserSecurityPaymentViewDto>> GetUserOrder(Guid id);

    }
}
