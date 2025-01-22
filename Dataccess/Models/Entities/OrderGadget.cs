using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class OrderGadget
    {
        public Guid Id { get; set; }
        public Guid PaymentId { get; set; }
        public Guid GadgetId { get; set; }
        public decimal price { get; set; }
        public virtual Gadget Gadget {  get; set; }  
        public virtual UserSecurityPayment UserPayment { get; set; }
    }
}
