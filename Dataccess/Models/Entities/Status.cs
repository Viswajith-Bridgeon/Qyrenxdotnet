namespace Qyrenx.Dataccess.Models.Entities
{
    public class Status:AuditableEntity
    {
        public Guid Id { get; set; }
        public Guid PickupId { get; set; }
        public string Statuss { get; set; }
        public virtual Pickup Pickup { get; set; }
    }
}
