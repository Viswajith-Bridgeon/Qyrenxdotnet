using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class DeliveryPersonOnline
    {
        public Guid Id { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public bool IsActive { get; set; } = DateTime.Now.Hour < 10|| DateTime.Now.Hour > 16 ? false : true;
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        [JsonIgnore]
        public virtual DeliveryPerson DeliveryPerson {  get; set; }
    }
}
