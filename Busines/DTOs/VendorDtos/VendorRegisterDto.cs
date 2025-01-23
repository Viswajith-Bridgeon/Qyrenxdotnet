using System.ComponentModel.DataAnnotations;

namespace Qyrenx.Business.Models.DTOs.VendorDtos
{
	public class VendorRegisterDto
	{
		[Required]
		public string? Name { get; set; }
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		public string Mobile { get; set; }
		[Required]
		public string? ShopeName { get; set; }
		[Required]
		public string? Password { get; set; }
        [Required]
        public string? House { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? LandMark { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        public string? otp { get; set; }

	}
}
