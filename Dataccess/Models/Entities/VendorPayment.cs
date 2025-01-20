using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class VendorPayment:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid VendorCostId { get; set; }
        public decimal Payment {  get; set; }
        public virtual VendorCost VendorCost { get; set; }

    }
}
