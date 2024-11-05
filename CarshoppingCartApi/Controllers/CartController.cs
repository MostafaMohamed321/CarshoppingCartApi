using CarshoppingCartApi.DTO;
using CarshoppingCartApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarshoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo cartRepo;
        public CartController(ICartRepo cartRepo)
        {
            this.cartRepo = cartRepo;
            
        }
        [HttpGet("AddItem")]
        public async Task<IActionResult> AddItem(int carId ,int qnt =1)
        {
            var cartCount = await cartRepo.AddItem(carId, qnt);
            return Ok("Add to Cart Successfully");
        }
        [HttpGet("RemoveItem")]
        public async Task<IActionResult> RemoveItem(int carId)
        {
            var item =await cartRepo.RemoveItem(carId);
            return Ok("RemoveItem Done");
            
        }
        [HttpGet("GetUserCart")]
        public async Task<IActionResult> GetUserCart(int carId)
        {
            var userCart = await cartRepo.GetUserCart();
            return Ok(userCart);
        }
        [HttpGet("GetCartItemCount")]

        public async Task<IActionResult> GetCartItemCount(int carId)
        {
            var carCount = await cartRepo.GetCartItemCount();
            return Ok(carCount);
        }
        [HttpPost("CheckOutModel")]
        public async Task<IActionResult> CheckOutModel(CheckOrderModel check)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            bool checkOut = await cartRepo.CheckOut(check);
            if (checkOut)
                return Ok("OrderSuccessfully");
            return BadRequest("order failure");
        }

    }
}
