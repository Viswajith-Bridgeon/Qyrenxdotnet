namespace Qyrenx.Dataccess.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }


        public string Name { get; set; }


        public string Email { get; set; }


        public int Mobile { get; set; }

        public string HashPassword { get; set; }
        public bool IsBlock { get; set; } = false;
        public string Role { get; set; } = "User";
        public DateTime Date { get; set; } =DateTime.Now;
    }
}
