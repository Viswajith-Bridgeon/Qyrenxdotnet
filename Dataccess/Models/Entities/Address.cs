using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class Address:AuditableEntity
    {
        public Guid Id {  get; set; }
        public Guid UserId { get; set; }    
        public string House {  get; set; }
        public string City { get; set; }
        public string LandMark { get; set; }
        public string PostalCode { get; set; }
        public virtual User Users { get; set; }
    }
}
