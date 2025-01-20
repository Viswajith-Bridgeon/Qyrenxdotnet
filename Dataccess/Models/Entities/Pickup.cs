namespace Qyrenx.Dataccess.Models.Entities
{
    public class Pickup:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid GadgetId { get; set; }
        public Guid VendorId { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public Guid? ReturnDeliveryPersonId { get; set; }=null;
        public virtual DeliveryPerson DeliveryPersons { get; set; }
        public virtual Vendor Vendors { get; set; }
        public virtual Gadget Gadget { get; set; }
        public ICollection<Status> Statuss { get; set; }
        public virtual VendorCost VendorCost { get; set; }
    }
}
