using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class PaymentToUser:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid Userid { get; set; }
        public Guid StatusId { get; set; }  
        public decimal Pay {  get; set; }

    }
}
