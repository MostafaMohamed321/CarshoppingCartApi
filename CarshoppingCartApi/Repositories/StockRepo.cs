using CarshoppingCartApi.DTO;
using CarshoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarshoppingCartApi.Repositories
{
    public class StockRepo : IStockRepo
    {
        private readonly CarShoppingDb db;
        public StockRepo(CarShoppingDb db)
        { 
           this.db = db;
        }
        public async Task<Stock> GetStockById(int Id)
        {
            var stock =await db.Stocks.FirstOrDefaultAsync(x => x.Id == Id);
            return stock;
        }
        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string NAme ="")
        {
            NAme =NAme.ToLower();
            var stocks = await (from car in db.cars
                                join stock in db.Stocks
                                on car.Id equals stock.CarId
                                into car_Stock
                                from carstock in car_Stock.DefaultIfEmpty()
                                where string.IsNullOrEmpty(NAme) || car.CarName.ToLower().StartsWith(NAme)
                                select new StockDisplayModel
                                {
                                    carId =car.Id,
                                    CarName=car.CarName,
                                    Quantity =car.Quantity ==null?0:car.Quantity,

                                }
                                 
                               ).ToListAsync();
            return stocks;

        }

        public async Task ManageStock(int id,StockManage stockDisplay)
        {
            var stock = await GetStockById(id);
            if (stock != null)
            {
                stock.Quantity = stockDisplay.Quantity;

            }
            else
            {
                Stock NewStock = new()
                {
                    Quantity =stockDisplay.Quantity,
                    CarId =stockDisplay.carId,
                    
                };
            }
            await db.SaveChangesAsync();

        }

      
    }
}
