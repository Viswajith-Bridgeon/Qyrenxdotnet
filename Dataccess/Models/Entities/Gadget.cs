namespace Qyrenx.Dataccess.Models.Entities
{
    public class Gadget:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string GadgetName { get; set; }
        public string Image { get; set; }
        public Guid AddressId { get; set; }
        public string Description { get; set; }
        public virtual User Users { get; set; }
        public virtual Pickup Pickup { get; set; }
        public virtual Category Category { get; set; }
        public virtual Address Address { get; set; }
        public virtual OrderGadget OrderGadget { get; set; }
    }
}
