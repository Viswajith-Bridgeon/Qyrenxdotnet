    namespace Qyrenx.Dataccess.Models.Entities
    {
    public class DeliveryPerson: AuditableEntityJwt
    {
        public Guid Id { get; set; }
        public Guid PickupId { get; set; }
        public string Name { get; set; }
        public string DrivingLicense { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string HashPassword { get; set; }
        public string Role { get; set; } = "DeliveryPerson";
        public bool IsBlock { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public ICollection<Pickup> Pickups { get; set; }
        public virtual DeliveryPersonOnline DeliveryPersonOnline { get; set; }

        //public virtual DeliveryPersonPayment DeliveryPersonPayment { get; set; }

    }
}
