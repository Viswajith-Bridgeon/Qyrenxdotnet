using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class VendorAddress :AuditableEntity
    {

        public Guid Id { get; set; }
        public Guid VendorId { get; set; }
        public string Role { get; set; }
        public string House { get; set; }
        public string City { get; set; }
        public string LandMark { get; set; }
        public string PostalCode { get; set; }
        public virtual Vendor Vendor { get; set; }

    }
}
