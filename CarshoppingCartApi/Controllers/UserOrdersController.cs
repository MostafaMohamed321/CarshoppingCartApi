using CarshoppingCartApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarshoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOrdersController : ControllerBase
    {
        private readonly IUserOrderRepo _userOrderRepo;
        public UserOrdersController(IUserOrderRepo userOrderRepo)
        {
            _userOrderRepo = userOrderRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserOrder() 
        {
            var getUserOrder = await _userOrderRepo.UserOrder();
            return Ok(getUserOrder);
            
        }
    }
}
