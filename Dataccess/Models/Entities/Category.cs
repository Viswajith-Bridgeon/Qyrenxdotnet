namespace Qyrenx.Dataccess.Models.Entities
{
    public class Category:AuditableEntity
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string Image { get; set; }
    }
}
