using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    internal class VendorCategory
    {
        public Guid Id { get; set; }    
        public Guid VendorId{ get; set; }
        public Guid CategoryId { get; set; }
       
    }
}
