using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class UserPayment:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid UserId  { get; set; }
        public Guid StatusId { get; set; }
        public decimal SecurityAmount {  get; set; }
        public decimal OriginalAmount { get; set; }
        public virtual User Users { get; set; }
        public virtual Status Status { get; set; }
    }
}
