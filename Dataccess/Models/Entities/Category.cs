namespace Qyrenx.Dataccess.Models.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
