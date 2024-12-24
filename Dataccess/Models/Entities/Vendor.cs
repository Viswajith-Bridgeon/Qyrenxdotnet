namespace Qyrenx.Dataccess.Models.Entities
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public int Mobile { get; set; }

        public string ShopeName { get; set; }

        public string ShopeLicense { get; set; }

        public string HashPassword { get; set; }
        public bool IsBlock { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public string Role { get; set; } = "Vendor";
        public DateTime Date { get; set; }= DateTime.Now;
        public ICollection<Category> Categories { get; set; }
    }
}
