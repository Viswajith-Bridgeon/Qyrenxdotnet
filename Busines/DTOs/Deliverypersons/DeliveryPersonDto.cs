namespace Qyrenx.Business.Models.DTOs.Deliverypersons
{
    public class DeliveryPersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DrivingLicense { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Role { get; set; } 
        public bool IsBlock { get; set; } 
        public bool IsVerified { get; set; }
    }
}
