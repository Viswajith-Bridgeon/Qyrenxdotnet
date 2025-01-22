using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Business.DTOs.Deliverypersons
{
    public class PickupVendorDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string GadgetName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public string DeliveryBoyName { get; set; }

        public int DeliveryBoyNumber { get; set; }
    }

}

