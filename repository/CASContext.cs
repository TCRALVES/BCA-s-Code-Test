using CAS.Models.DBEntities;
using Microsoft.EntityFrameworkCore;
using Models.Models.DBEntities;

namespace repository
{
    public class CASContext : DbContext
    {
        public CASContext() { }

        public string ConnectionString { get; set; }

        public CASContext(DbContextOptions<CASContext> options, string connectionString = "") : base(options)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
                ConnectionString = connectionString;

            var envVar = Environment.GetEnvironmentVariable("CAS_CONNECTION_STRING", EnvironmentVariableTarget.Machine);

            if (!string.IsNullOrWhiteSpace(envVar))
                ConnectionString = envVar;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                ConnectionString = Environment.GetEnvironmentVariable("CAS_CONNECTION_STRING", EnvironmentVariableTarget.Machine);

                if (string.IsNullOrWhiteSpace(ConnectionString))
                    throw new Exception();
            }

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConnectionString);
        }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().UseTptMappingStrategy();
        }
        #endregion


        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<SUV> SUVs { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Sedan> Sedans { get; set; }
        public DbSet<Hatchback> Hatchbacks { get; set; }
        public DbSet<AuctionedVehicle> AuctionedVehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }
}