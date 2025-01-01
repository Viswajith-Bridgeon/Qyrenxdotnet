using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.Deliverypersons
{
    public class DeliveryPersonOnlineDto
    {
        public Guid DeliveryPersonId { get; set; }
        public bool IsActive { get; set; } = DateTime.Now.Hour < 10 || DateTime.Now.Hour > 16 ? false : true;
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
    }
}
