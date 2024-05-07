using Microsoft.EntityFrameworkCore;
using POSIndexer.Models;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace POSIndexer.Migrations
{
    public class InventoryReadDB : DbContext
    {
        public InventoryReadDB() { }
        public InventoryReadDB(DbContextOptions options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Part> Parts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryReadDB).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }

}