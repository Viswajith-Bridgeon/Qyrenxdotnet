namespace Qyrenx.Dataccess.Models.Entities
{
    public class Gadget
    {
        public Guid Id { get; set; }
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
        public string GadgetName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public virtual Service service {  get; set; }
        public ICollection<User> users { get; set; }

    }
}
