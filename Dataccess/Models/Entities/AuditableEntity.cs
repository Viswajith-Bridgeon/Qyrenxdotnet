using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public abstract class AuditableEntity
    {
        public DateTime? CreatedOn {  get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsDelete { get; set; } = false;
        public string? DeletedBy { get; set; }

    }
}
