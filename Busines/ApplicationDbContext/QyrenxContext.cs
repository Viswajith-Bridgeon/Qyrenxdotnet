

using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Qyrenx.Dataccess.Models.Entities;

namespace Qyrenx.Business.ApplicationDbContext
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
            //user admin seeding
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
            modelBuilder.Entity<User>().
                HasKey(x => x.Id);
            
            modelBuilder.Entity<User>().
                HasMany(p => p.Gadgets).
                WithOne(p => p.users).
                HasForeignKey(p=>p.UserId);

            //vendor
            modelBuilder.Entity<Vendor>().
                HasKey(x => x.Id);




        }
    }
}
