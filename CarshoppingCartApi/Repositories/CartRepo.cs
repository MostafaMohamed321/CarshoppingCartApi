using CarshoppingCartApi.DTO;
using CarshoppingCartApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CarshoppingCartApi.Repositories
{
    public class CartRepo: ICartRepo
    {
        private readonly CarShoppingDb db;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;
        public CartRepo(CarShoppingDb db,IHttpContextAccessor http,UserManager<IdentityUser> user)
        {
            this.db = db;
            httpContextAccessor = http;
            userManager = user;
        }
        public async Task<int> AddItem(int carId, int qty)
        {
            string userId = GetUserId();

            using var transaction = db.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException("User not Logged-in");

                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = userId

                    };
                    db.ShoppingCarts.Add(cart);

                }
                db.SaveChanges();


                var cartItem = db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.CarId == carId);
                if (cartItem is not null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var car = db.cars.Find(carId);
                    cartItem = new CartDetail
                    {
                        CarId = carId,
                        ShoppingCartId = cart.Id,
                        Quantity = qty,
                        UnitPrice = car.Price
                    };
                    db.CartDetails.Add(cartItem);
                }
                db.SaveChanges();
                transaction.Commit();

            }
            catch (Exception ex)
            {

            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }
        public async Task<int> RemoveItem(int carId)
        {
            var user = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(user))
                    throw new Exception("user not logged_In");
                var cart = await db.ShoppingCarts.FirstOrDefaultAsync(a => a.UserId == user);
                if (cart is null)
                    throw new Exception("Invalid Cart");
                var cartItem = db.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id);
                if (cartItem is null)
                    throw new Exception("cart is Empty");
                else if (cartItem.Quantity == 1)
                    db.CartDetails.Remove(cartItem);
                else
                    cartItem.Quantity = cartItem.Quantity - 1;
                
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            var CartItem =await GetCartItemCount(user);
            return CartItem;


        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var user = GetUserId();
            if (user == null)
                throw new UnauthorizedAccessException("Invalid UserId");
            var ShoppingCart = await db.ShoppingCarts.Include(a => a.CartDetails)
                                                        .ThenInclude(a => a.Car)
                                                        .ThenInclude(a => a.Stock)
                                                        .Include(a => a.CartDetails)
                                                        .ThenInclude(a => a.Car)
                                                        .ThenInclude(a => a.Genre)
                                                        .Where(a => a.UserId == user)
                                                        .FirstOrDefaultAsync();


            return ShoppingCart;

        }
        public async Task<int> GetCartItemCount(string? userId ="")
        {
            if (!string.IsNullOrEmpty(userId)) 
            {
                userId = GetUserId();
            }
            var qnt = await(from carts in db.ShoppingCarts
                            join cartd in db.CartDetails
                            on carts.Id equals cartd.ShoppingCartId
                            select new {cartd.Id}
                           ).ToListAsync();
            return qnt.Count;

        }
        public async Task<bool> CheckOut(CheckOrderModel checkOrder)
        {
            using var Transaction = db.Database.BeginTransaction();
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException("UserId is Logged_In");
                var cart =GetCart(userId);
                if (cart == null)
                    throw new UnauthorizedAccessException("Invalid Cart");
                var cartDetail =db.CartDetails.Where(a=>a.ShoppingCartId ==cart.Id);
                if (cartDetail.Count() == 0)
                    throw new UnauthorizedAccessException("cart is Empty");
                var pending = db.orderStatuses.FirstOrDefault(x => x.StatusName == "pending");
                if (pending == null)
                    throw new UnauthorizedAccessException("not have pending");
                var order = new Order
                {
                    UserId = userId,
                    Name = checkOrder.Name,
                    MobileNumber = checkOrder.MobileNumber,
                    Email = checkOrder.Email,
                    Address = checkOrder.Address,
                    IsPaid =false,
                    PaymentMethod = checkOrder.PaymentMethod,
                    CreateDate = DateTime.Now,
                    OrderStatusId=pending.Id,

                };
                db.Add(order);
                db.SaveChanges();
                foreach (var item in cartDetail)
                {
                    var orderDetail = new OrderDetails
                    {
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        CarId = item.CarId,
                        OrderId=order.Id,
                    };
                    var stock = await db.Stocks.FirstOrDefaultAsync(a=>a.CarId==item.CarId);
                    if (stock == null)
                        throw new Exception("car out of stock");
                    stock.Quantity -= item.Quantity;
                }
                db.CartDetails.RemoveRange(cartDetail);
                db.SaveChanges();
                Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<ShoppingCart?> GetCart(string userId)
        {
            var shCart =await db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return shCart;
        }
        private string? GetUserId()
        {
            var principal = httpContextAccessor?.HttpContext?.User;
            if (principal != null)
            {
                string? userId = userManager.GetUserId(principal);
                return userId;

            }
            return null;
        }

    }
}
