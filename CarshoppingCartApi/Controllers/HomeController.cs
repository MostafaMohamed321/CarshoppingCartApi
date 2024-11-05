using CarshoppingCartApi.Const;
using CarshoppingCartApi.DTO;
using CarshoppingCartApi.Models;
using CarshoppingCartApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarshoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeController : ControllerBase
    {
        private readonly IHomeRepo HomeRepo;
        public HomeController(IHomeRepo C)
        {
            HomeRepo = C;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var car = await HomeRepo.GetCars();


            return Ok(car);
        }
        [HttpGet]
        public async Task<IActionResult> GetCarByName(string carName ,int genreId)
        {
            var f =await HomeRepo.GetCarByAnyString(carName, genreId);
            if(f == null)
                return NotFound();
            return Ok(f);

        }

            
        
    }
}
