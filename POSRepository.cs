using POSIndexer.Migrations;
using POSIndexer.Models;

namespace POSIndexer
{
    public class POSRepository
    {
        private readonly InventoryReadDB _db;
        public POSRepository()
        {
            _db = new InventoryReadDB();
        }
        public void AddCar(Car car)
        {
            _db.Cars.Add(car);
            _db.SaveChanges();
        }
        public void UpdateCar(Car car)
        {
            _db.Cars.Update(car);
            _db.SaveChanges();
        }
    }
}
