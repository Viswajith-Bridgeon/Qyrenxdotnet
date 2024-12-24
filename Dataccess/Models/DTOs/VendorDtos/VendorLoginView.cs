namespace Qyrenx.Dataccess.Models.DTOs.VendorDtos
{
	public class VendorLoginView
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
		public string Error { get; set; }
		public bool Isblocked { get; set; }
		public string Token { get; set; }
	}
}
