namespace Qyrenx.Dataccess.Models.Entities
{
    public class Service
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public Guid categoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
