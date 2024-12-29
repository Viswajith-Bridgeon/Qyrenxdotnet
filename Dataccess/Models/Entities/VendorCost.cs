using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class VendorCost:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid PickupId { get; set; }
        public Guid VendorId { get; set; }
        public string ProblemDescription {  get; set; }
        public decimal Cost {  get; set; }
        public virtual Pickup Pickups { get; set; }
        public virtual Vendor Vendors { get; set; }

    }
}
