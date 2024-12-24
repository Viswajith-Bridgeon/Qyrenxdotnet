

using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.Models.Entities;

namespace Qyrenx.Dataccess.ApplicationDbContext
{
    public class QyrenxContext: DbContext
    {
        public QyrenxContext(DbContextOptions<QyrenxContext>options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<Vendor> Vendors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("a1f5d5da-e94d-44f1-a8c3-b60f42101a01"),
                Name = "Admin",
                Email = "admin@gmail.com",
                HashPassword = BCrypt.Net.BCrypt.HashPassword("admin@1234"),
                Role = "Admin",
                IsBlock = false,
                Mobile = 1234567890,
                Date = new DateTime(2024, 1, 1)
            });

        }
    }
}
