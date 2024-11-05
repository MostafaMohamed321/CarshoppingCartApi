using CarshoppingCartApi.DTO;
using CarshoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarshoppingCartApi.Repositories
{
    public class HomeRepo:IHomeRepo
    {
        private readonly CarShoppingDb db;
        public HomeRepo(CarShoppingDb dp)
        {
            db = dp;
        }
        public async Task<IEnumerable<Car>> GetCars() 
        {
           return  await db.cars.ToListAsync();
          
            
           
        
          
        
        }
        public async Task<IEnumerable<Car>> GetCarByAnyString(string N,int genreId)
        {
            N =N.ToLower();
            var c = await (from car in db.cars
                           join genre in db.Genres
                           on car.GenreId equals genre.Id
                           join stock in db.Stocks
                           on car.Id equals stock.CarId
                           into stocks_Car
                           from carStock in stocks_Car.DefaultIfEmpty()
                           where string.IsNullOrEmpty(N) || (car != null && car.CarName.ToLower().StartsWith(N))
                           select new Car
                           {
                               Id = car.Id,
                               CarName = car.CarName,
                               GenreId =car.GenreId,
                               Price = car.Price,
                               Image = car.Image,
                               GenreName = genre.GenreName,
                               Quantity = carStock.Quantity==null ?0:carStock.Quantity,
                           }
                           ).ToListAsync();
            return c;
        }
    }
}
