using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class VendorCategory :AuditableEntity
    {
        public Guid Id { get; set; }    
        public Guid VendorId{ get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Vendor Vendor { get; set; }
       
    }
}
