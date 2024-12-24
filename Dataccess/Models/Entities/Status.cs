namespace Qyrenx.Dataccess.Models.Entities
{
    public class Status
    {
        public Guid Id { get; set; }
        public Guid PickupId { get; set; }
        public bool processing { get; set; }
        public bool PickupbyDelivery { get; set; }
        public string ReceivedByVendor { get; set; } 
        public bool success {  get; set; }
        public int payment {  get; set; }
        public virtual Pickup Pickup { get; set; }
    }
}
