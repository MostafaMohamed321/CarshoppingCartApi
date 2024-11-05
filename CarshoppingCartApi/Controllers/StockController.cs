using CarshoppingCartApi.Const;
using CarshoppingCartApi.DTO;
using CarshoppingCartApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarshoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =nameof(Role.Admin))]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo stockRepo;
        public StockController(IStockRepo stockRepo)
        {
            this.stockRepo = stockRepo;
        
        }

        [HttpGet("GetStocks")]
        public async Task<IActionResult> GetStocks(string name)
        {
            var stocks = await stockRepo.GetStocks(name);
            if (stocks != null)
            {
                return Ok(stocks);
            }
            return BadRequest();
        
        }

        [HttpPost("id")]
        public async Task<IActionResult> ManageStock(int id,StockManage displayModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                await stockRepo.ManageStock(id,displayModel);
                return Ok("Update SuccessFully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
