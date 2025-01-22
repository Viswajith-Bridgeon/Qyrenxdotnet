using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class AccountsVendorDelivery:AuditableEntity
    {
        public Guid Id { get; set; }
        public string Role {  get; set; }
        public Guid PersonId { get; set; }
        public string IFSC {  get; set; }
        public string AccountNo { get; set; }
    }
}
