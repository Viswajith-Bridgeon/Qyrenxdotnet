using Microsoft.EntityFrameworkCore;
using Qyrenx.Models.Entities;

namespace Qyrenx.ApplicationDbContext
{
    public class QyrenxContext:DbContext
    {
        public QyrenxContext(DbContextOptions<QyrenxContext>options):base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<DeliveryPerson> DeliveryPersons { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

    }
}
