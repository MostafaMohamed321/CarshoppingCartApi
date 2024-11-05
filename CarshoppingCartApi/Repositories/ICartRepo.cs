using CarshoppingCartApi.DTO;
using CarshoppingCartApi.Models;

namespace CarshoppingCartApi.Repositories
{
    public interface ICartRepo
    {
        Task<int> AddItem(int bookId, int qty);
        Task<int> RemoveItem(int bookId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string? userId = "");
        Task<bool> CheckOut(CheckOrderModel checkOrder);
        Task<ShoppingCart?> GetCart(string userId);
    }
}