using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.UserSecurityPaymentDto
{
    public class UserSecurityPaymentViewDto
    {
          public string UserName { get; set; }
        public string Address { get; set; }
        public string Img {  get; set; }    
        public decimal SecurityAmount { get; set; }

    }
}
