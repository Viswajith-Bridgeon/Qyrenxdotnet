using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class DeliveryPersonOnline
    {
        public Guid Id { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public bool IsActive { get; set; } = false;
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public virtual DeliveryPerson DeliveryPerson {  get; set; }
    }
}
