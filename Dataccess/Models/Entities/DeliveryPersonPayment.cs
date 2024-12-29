using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class DeliveryPersonPayment:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public bool SalaryStatus {  get; set; }
        public bool BonusStatus {  get; set; }
        public virtual DeliveryPerson Person { get; set; }

    }
}
