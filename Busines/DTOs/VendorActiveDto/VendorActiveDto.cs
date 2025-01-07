using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.VendorActiveDto
{
    public class VendorActiveDto
    {
        public Guid VendorId { get; set; }
        public decimal Lat {  get; set; }
        public decimal Long { get; set; }
    }
}
