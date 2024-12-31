

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
        public DbSet<DeliveryPersonOnline>DeliveryPersonOnlines { get; set; }
        public DbSet<DeliveryPersonPayment> deliveryPersonPayments { get; set; }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorCategory> VendorCategories { get; set; }
        public DbSet<VendorCost> VendorCost { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Gadget> Gadgets { get; set; }
        public DbSet<Pickup> Pickups { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<PaymentToUser> PaymentToUsers { get; set; }
        public DbSet<UserPayment> UserPayment { get; set; }
        public DbSet<VendorPayment> VendorPayment { get; set; } 
        public DbSet<AccountsVendorDelivery> AccountsVendorDeliveries { get; set; }

        
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
            });


            modelBuilder.Entity<User>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<DeliveryPerson>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Vendor>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Pickup>()
                 .HasOne(p => p.DeliveryPersons)
                 .WithMany(p => p.Pickups)
                 .HasForeignKey(p => p.DeliveryPersonId);

            modelBuilder.Entity<DeliveryPerson>().
                HasOne(p => p.DeliveryPersonOnline).
                WithOne(p => p.DeliveryPerson);

            modelBuilder.Entity<DeliveryPersonOnline>().
                HasOne(p=>p.DeliveryPerson).
                WithOne(p => p.DeliveryPersonOnline). 
                HasForeignKey<DeliveryPersonOnline>(p => p.DeliveryPersonId);
     

            modelBuilder.Entity<User>().
                HasMany(p=>p.Gadgets).
                WithOne(p => p.Users).
                HasForeignKey(p=>p.UserId);

           
            modelBuilder.Entity<Pickup>().
                HasOne(p=>p.Gadget).
                WithOne(p=>p.Pickup).
                HasForeignKey<Pickup>(p => p.GadgetId);   

            modelBuilder.Entity<Pickup>().
                HasOne(p=>p.Status).
                WithOne (p=>p.Pickup).
                HasForeignKey<Status>(p => p.PickupId);

           modelBuilder.Entity<Address>().
                HasMany(p=>p.Gadgets).
                WithOne(p=>p.Address).
                HasForeignKey (p=>p.AddressId);
            


            //modelBuilder.Entity<DeliveryPersonPayment>().
            //    HasOne(p => p.Person).
            //    WithOne(p => p.DeliveryPersonPayment);
            //modelBuilder.Entity<Vendor>().
            //    HasMany(p => p.VendorPayment).
            //    WithOne(p => p.Vendor);
            //modelBuilder.Entity<Vendor>().
            //    HasMany(p=>p.VendorPayment).
            //    WithOne(p=>p.Vendor);


                


        }
    }
}
