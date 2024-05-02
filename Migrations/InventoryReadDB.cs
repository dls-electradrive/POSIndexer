using Microsoft.EntityFrameworkCore;
using POSIndexer.Models;
using System.Runtime.CompilerServices;

namespace POSIndexer.Migrations
{
    public class InventoryReadDB : DbContext
    {
        public InventoryReadDB() { }
        public InventoryReadDB(DbContextOptions options) : base(options) { }


        public DbSet<Car> Cars { get; set; }

    }

}