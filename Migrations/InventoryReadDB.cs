using Microsoft.EntityFrameworkCore;
using POSIndexer.Models;
using System.Runtime.CompilerServices;

namespace POSIndexer.Migrations
{
    public class InventoryReadDB : DbContext
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        public InventoryReadDB() { }
        public InventoryReadDB(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        public DbSet<StandardCar> StandardCars { get; set; }
        public DbSet<CustomCar> CustomCars { get; set; }

    }

}