using POSIndexer.Migrations;
using POSIndexer.Models;

namespace POSIndexer
{
    public class POSRepository : IPOSRepository
    {
        private readonly InventoryReadDB _db;
        public POSRepository(InventoryReadDB db)
        {
            _db = db;
        }
        public void AddCar(Car car)
        {
            var cars  = _db.Cars.ToList();
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
