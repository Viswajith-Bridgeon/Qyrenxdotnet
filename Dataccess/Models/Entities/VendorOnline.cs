using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class VendorOnline
    {
        public Guid Id { get; set; }
        public Guid VendorId { get; set; }
        public bool IsActive {  get; set; }
        public decimal Lat {  get; set; }
        public decimal Long { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
