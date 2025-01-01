using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class UserSecurityPayment : AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal SecurityAmount { get; set; }
        public string PaymentString { get; set; }
        public string TransactionId { get; set; }
        public virtual User Users { get; set; }
        public virtual Address Addresses { get; set; }
        public virtual Gadget Gadgets { get; set; }
        public ICollection <OrderGadget> orderGadgets{ get; set;}
    }
}
