using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Qyrenx.Dataccess.Models.Entities
{
    public class VendorCost:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid PickupId { get; set; }
        public Guid VendorId { get; set; }
        public string ProblemDescription {  get; set; }
        public bool IsVenorServiceable {  get; set; }=true;
        public decimal ServiceCost {  get; set; }
        public decimal? SaleCost { get; set; }
        public bool IsServices {  get; set; }=false;
        public virtual Pickup Pickups { get; set; }
        public virtual Vendor Vendors { get; set; }


    }
}
