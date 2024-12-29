namespace Qyrenx.Dataccess.Models.Entities
{
    public class Pickup:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid GadgetId { get; set; }
        public Guid StatusId { get; set; }
        public Guid VendorId { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public virtual DeliveryPerson DeliveryPersons { get; set; }
        public virtual Vendor Vendors { get; set; }
        public virtual Gadget Gadget { get; set; }
        public virtual Status Status { get; set; }
    }
}
