using POSIndexer.Models;

namespace POSIndexer
{
    public interface IPOSRepository
    {
        void AddCar(Car car);
        void UpdateCar(Car car);
    }
}