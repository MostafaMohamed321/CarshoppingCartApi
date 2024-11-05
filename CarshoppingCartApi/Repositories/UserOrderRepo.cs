using CarshoppingCartApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarshoppingCartApi.Repositories
{
    public class UserOrderRepo:IUserOrderRepo
    {
        private readonly CarShoppingDb db;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public UserOrderRepo(CarShoppingDb db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        public async Task<IEnumerable<Order>> UserOrder(bool getAll = false)
        {
            var orders = db.Orders.Include(a=>a.OrderStatus)
                                  .Include(a=>a.OrderDetail).
                                    ThenInclude(a=>a.Car)
                                    .ThenInclude(a=>a.Genre)
                                    .AsQueryable();
            if (!getAll)
            {
                string? userId = UserId();
                if (userId == null)
                {
                    throw new Exception("UserId not Logged-In ");
                }
                orders.Where(o => o.UserId == userId);
                return await orders.ToListAsync();
            }
            return await orders.ToListAsync();
        }
        public async Task<Order?> GetOrderById(int id)
        {
            return await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            
            
            
           
        } 


        public string? UserId() 
        {
            var principal = httpContextAccessor.HttpContext.User;
            string userId= userManager.GetUserId(principal);
            return userId;
        }
    }
}
