using System.ComponentModel.DataAnnotations;

namespace Qyrenx.Models.DTOs.VendorDtos
{
	public class VendorRegisterDto
	{
		[Required]
		public string? Name { get; set; }
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		public int? Mobile { get; set; }
		[Required]
		public string? ShopeName { get; set; }
		[Required]
		public string? Password { get; set; }
	}
}
