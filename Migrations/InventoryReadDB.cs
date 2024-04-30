using Microsoft.EntityFrameworkCore;
using POSIndexer.Models;
using System.Runtime.CompilerServices;

namespace POSIndexer.Migrations
{
    public class InventoryReadDB : DbContext
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "server=host.docker.internal;port=3307;uid=root;pwd=12345;database=carstorage";
        public InventoryReadDB() { }
        public InventoryReadDB(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        public DbSet<Car> Cars { get; set; }

    }

}