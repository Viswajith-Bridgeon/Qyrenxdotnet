namespace Qyrenx.Dataccess.Models.Entities
{
    public class Pickup
    {
        public Guid Id { get; set; }
        public Guid GadgetId { get; set; }
        public Guid StatusId { get; set; }
        public virtual Gadget Gadget { get; set; }
        public virtual Status Status { get; set; }
    }
}
