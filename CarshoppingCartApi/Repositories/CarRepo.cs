using CarshoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarshoppingCartApi.Repositories
{
    public class CarRepo:ICarRepo
    {
        private readonly CarShoppingDb _db;
        public CarRepo(CarShoppingDb db)
        {
            _db = db;
        }
       
        public async Task<Car> GetCarById(int id)
        {
            var car = await _db.cars.FirstOrDefaultAsync(a=>a.Id == id);
            return car;

        }
        public async Task AddCar(Car car)
        {

             _db.cars.Add(car);
            await _db.SaveChangesAsync();

            
        }
        public async Task DeleteCar(Car car)
        {
            _db.cars.Remove(car);
           await _db.SaveChangesAsync();
            
        }
        public async Task UpdateCar(Car car)
        {
            _db.Update(car);
            await _db.SaveChangesAsync();
        }
    }
}
