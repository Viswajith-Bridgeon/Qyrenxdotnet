namespace Qyrenx.Business.Models.DTOs.VendorDtos
{
	public class VendorAdminViewDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public string Email { get; set; }

		public int Mobile { get; set; }

		public string ShopeName { get; set; }

		public string ShopeLicense { get; set; }
        public bool IsBlock { get; set; }
        //public string House { get; set; }
        //public string City { get; set; }
        //public string LandMark { get; set; }
        //public string PostalCode { get; set; }
        public DateTime Date { get; set; }


	}
}
