using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    internal class DeliveryPersonOnline
    {
        public Guid DeliveryPersonId { get; set; }
        public bool IsActive { get; set; }=false;
        public decimal? Lat {  get; set; }
        public decimal? Long { get; set; }
    }
}
