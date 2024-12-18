namespace Qyrenx.Models.Entities
{
    public class DeliveryPerson
    {
        public Guid Id { get; set; }
        public int DeliveryLocationZipcode { get; set; }
        public string Name { get; set; }
        public string DrivingLicense { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string HashPassword { get; set; }
        public string Role { get; set; } = "DeliveryPerson";
        public bool IsBlock { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
