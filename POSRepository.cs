using POSIndexer.Migrations;

namespace POSIndexer
{
    public class POSRepository
    {
        private readonly string _connectionstring = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        public void AddToDB(string writeobject, InventoryReadDB db)
        {
        }
    }
}
