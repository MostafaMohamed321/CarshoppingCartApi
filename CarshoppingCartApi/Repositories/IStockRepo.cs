using CarshoppingCartApi.DTO;
using CarshoppingCartApi.Models;

namespace CarshoppingCartApi.Repositories
{
    public interface IStockRepo
    {
        Task<Stock> GetStockById(int Id);
        Task<IEnumerable<StockDisplayModel>> GetStocks(string NAme = "");
        Task ManageStock(int id,StockManage stockDisplay);
    }

}