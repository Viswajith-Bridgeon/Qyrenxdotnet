

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
        //public DbSet<DeliveryPersonPayment> deliveryPersonPayments { get; set; }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorCategory> VendorCategories { get; set; }
        public DbSet<VendorCost> VendorCost { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Gadget> Gadgets { get; set; }
        public DbSet<Pickup> Pickups { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<VendorAddress> VendorAddresses { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<PaymentToUser> PaymentToUsers { get; set; }
        public DbSet<UserSecurityPayment> UserPayment { get; set; }
        public DbSet<VendorPayment> VendorPayment { get; set; } 
        public DbSet<AccountsVendorDelivery> AccountsVendorDeliveries { get; set; }
        public DbSet<OrderGadget> OrderGadgets { get; set; }
        public DbSet<VendorOnline> VendorOnline { get; set; }
        
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
            modelBuilder.Entity<Address>().
                HasQueryFilter(p=> !p.IsDelete);
            modelBuilder.Entity<Pickup>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<VendorPayment>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Gadget>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<VendorAddress>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<Status>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<AccountsVendorDelivery>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<DeliveryPersonPayment>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<PaymentToUser>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<VendorCategory>().
                HasQueryFilter(p => !p.IsDelete);
            modelBuilder.Entity<VendorCost>().
                HasQueryFilter(p => !p.IsDelete);

            //----------------------------------------------------------USER-----------------------------------------------------------
            //User Gadget----------------------------------
            modelBuilder.Entity<User>().
                HasMany(p => p.Gadgets).
                WithOne(p => p.Users).
                HasForeignKey(p => p.UserId);

            // User - UserSecurityPayment (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserSecurityPayment)
                .WithOne(usp => usp.Users)
                .HasForeignKey(usp => usp.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Assuming cascading delete is desired
            
            //User Address
            modelBuilder.Entity<User>()
                .HasMany(a=>a.Address).
                WithOne(a=>a.User)
                .HasForeignKey(a=>a.UserId).OnDelete(DeleteBehavior.NoAction);


            //----------------------------------------------------------DeliveryPerson-----------------------------------------------------------

            modelBuilder.Entity<DeliveryPerson>().
             HasOne(p => p.DeliveryPersonOnline).
             WithOne(p => p.DeliveryPerson);

            modelBuilder.Entity<DeliveryPersonOnline>().
                HasOne(p => p.DeliveryPerson).
                WithOne(p => p.DeliveryPersonOnline).
                HasForeignKey<DeliveryPersonOnline>(p => p.DeliveryPersonId).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Pickup>()
               .HasOne(p => p.DeliveryPersons)
               .WithMany(p => p.Pickups)
               .HasForeignKey(p => p.DeliveryPersonId).OnDelete(DeleteBehavior.NoAction);


            //----------------------------------------------------------Vendor-----------------------------------------------------------

            modelBuilder.Entity<Vendor>()
             .HasMany(e => e.Pickups)
             .WithOne(e => e.Vendors)
             .HasForeignKey(e => e.VendorId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vendor>()
                .HasMany(e => e.VendorCosts)
                .WithOne(e => e.Vendors)
                .HasForeignKey(e => e.VendorId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Vendor>()
                .HasOne(e=>e.VendorAddress)
                .WithOne(e=>e.Vendor)
                .HasForeignKey<VendorAddress>(e=>e.VendorId);
            modelBuilder.Entity<VendorOnline>()
                .HasOne(p=>p.Vendor)
                .WithOne(p=>p.VendorOnline)
                .HasForeignKey<VendorOnline>(e=>e.VendorId).OnDelete(DeleteBehavior.NoAction);


            //---------------------------------Gadget-------------------------------------------------------------

            //------------------------------------------Pickup-------------------------------------

            modelBuilder.Entity<Pickup>().
                HasOne(p=>p.Gadget).
                WithOne(p=>p.Pickup).
                HasForeignKey<Pickup>(p => p.GadgetId);


            //-----------------------------------------status-----------------------------------------------------

            modelBuilder.Entity<Status>().
                HasOne(p => p.Pickup).
                WithMany(p => p.Statuss).
                HasForeignKey(p => p.PickupId).OnDelete(DeleteBehavior.NoAction);

           modelBuilder.Entity<Address>().
                HasMany(p=>p.Gadgets).
                WithOne(p=>p.Address).
                HasForeignKey (p=>p.AddressId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserSecurityPayment>().
                Property(t=>t.SecurityAmount).
                HasPrecision(18,2);

            modelBuilder.Entity<OrderGadget>().
                Property(t=>t.price).
                HasPrecision(18,2);

            modelBuilder.Entity<VendorCost>().
                Property(c=>c.ServiceCost).HasPrecision(18,2);

            modelBuilder.Entity<VendorCost>().
               Property(c => c.SaleCost).HasPrecision(18, 2);



            // UserSecurityPayment - OrderGadget (One-to-One)
            modelBuilder.Entity<UserSecurityPayment>()
                .HasOne(usp => usp.orderGadgets)
                .WithOne(og => og.UserPayment)
                .HasForeignKey<OrderGadget>(og => og.PaymentId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict cascading delete to avoid the cycle


            // OrderGadget - Gadget (One-to-One)
            modelBuilder.Entity<OrderGadget>()
                .HasOne(og => og.Gadget)
                .WithOne(o => o.OrderGadget)
                .HasForeignKey<OrderGadget>(og => og.GadgetId)
                .OnDelete(DeleteBehavior.Cascade); // Assuming cascading delete is desired


            //

        }
    }
}
