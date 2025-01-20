using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.ApplicationDbContext;
using Qyrenx.Dataccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.DbAccess.UserSecurityPay

{
    public class UserSecurityRepo: IuserSecurityRepo
    {
        private readonly QyrenxContext _context;
        public UserSecurityRepo(QyrenxContext context)
        {
            _context = context;
        }
        public async Task<List<UserSecurityPayment>> GetAllUserSecurityPayment()
        {
            try
            {
                var data = await _context.UserPayment.ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }
}
