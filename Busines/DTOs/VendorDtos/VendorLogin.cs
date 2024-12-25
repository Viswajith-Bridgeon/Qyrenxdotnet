using System.ComponentModel.DataAnnotations;

namespace Qyrenx.Business.Models.DTOs.VendorDtos
{
	public class VendorLogin
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
		[Required]
		public string? Password { get; set; }
	}
}
