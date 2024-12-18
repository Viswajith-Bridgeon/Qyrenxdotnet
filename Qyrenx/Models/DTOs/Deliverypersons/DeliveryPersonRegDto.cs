using System.ComponentModel.DataAnnotations;

namespace Qyrenx.Models.DTOs.Deliverypersons
{
    public class DeliveryPersonRegDto
    {
        [Required]
        public int DeliveryLocationZipcode { get; set; }
        [Required]

        public string DeliveryPersonName { get; set; }

        [Required]

        public string DrivingLicense { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Mobile { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }
        //[Required]
        //public int Otp { get; set; }
    }
}
