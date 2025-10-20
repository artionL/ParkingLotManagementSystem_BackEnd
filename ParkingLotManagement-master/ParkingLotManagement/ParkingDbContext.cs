using Microsoft.EntityFrameworkCore;
using ParkingLotManagement.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace ParkingLotManagement
{
    public class ParkingDbContext : DbContext
    {
        public ParkingDbContext()
        { }

        public ParkingDbContext(DbContextOptions<ParkingDbContext> options) :
     base(options)
        { }

        public DbSet<ParkingSpots> ParkingSpots { get; set; }
        public DbSet<PricingPlans> PricingPlans { get; set; }
        public DbSet<Subscribers> Subscribers { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Logs> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
