using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.VendorDtos
{
    public class VendorCostView
    {
        public Guid PickupId { get; set; }
        public string VendorName { get; set; }
        public int VendorPhone { get; set; }
        public string ProblemDescription { get; set; }
        public bool IsServiceable { get; set; } = true;
        public decimal ServiceCost { get; set; }
        public decimal? SaleCost { get; set; }
    }
}
