using CarshoppingCartApi.Models;

namespace CarshoppingCartApi.Repositories
{
    public interface IUserOrderRepo
    {
       Task<IEnumerable<Order>> UserOrder(bool getAll = false);
        Task<Order?> GetOrderById(int id);
    }
}
