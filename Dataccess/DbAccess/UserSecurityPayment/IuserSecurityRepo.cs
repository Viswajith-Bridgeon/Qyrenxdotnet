using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.UserSecurityPay
{
    public interface IuserSecurityRepo
    {
        Task<List<UserSecurityPayment>> GetAllUserSecurityPayment();
    }
}
