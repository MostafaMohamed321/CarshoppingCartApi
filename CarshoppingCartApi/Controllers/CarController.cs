using CarshoppingCartApi.DTO;
using CarshoppingCartApi.Models;
using CarshoppingCartApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarshoppingCartApi.Const;
using System.Data;
namespace CarshoppingCartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles=nameof(Role.Admin))]
    public class CarController : ControllerBase
    {
        private readonly ICarRepo carRepo;
        private readonly IGenreRepo genreRepo;
        public CarController(ICarRepo carRepo,IGenreRepo g)
        {
            this.carRepo = carRepo;
            this.genreRepo = g;
        }
        [HttpPost("AddCar")]
        public async Task<IActionResult> AddCar([FromForm]CarDto carDto)
        {   
            
            if (ModelState.IsValid)
            {
                using var stream = new MemoryStream();

                await carDto.ImageFile.CopyToAsync(stream);

               
                Car car = new()
                {
                    
                    CarName = carDto.CarName,
                    Price = carDto.Price,
                    GenreId = carDto.GenreId,
                    Image = stream.ToString(),
                };
                await carRepo.AddCar(car);
                return Ok(car);
            }
            return BadRequest();
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var carDelete =await carRepo.GetCarById(id);
            if (carDelete != null)
            {
                await carRepo.DeleteCar(carDelete);
                return Ok("Car  Deleted");
            }
            return BadRequest();
           
        }
        [HttpPut("id")]
        public async Task <IActionResult> UpdateCar(int id, [FromForm] CarDto carDto)
        {
            var getCar = await carRepo.GetCarById(id);
            using var stream = new MemoryStream();
            await carDto.ImageFile.CopyToAsync(stream);
            if(getCar != null && ModelState.IsValid)
            { 
                getCar.CarName = carDto.CarName;
                getCar.Price = carDto.Price;
                getCar.Image =carDto.ImageFile.ToString();
                getCar.GenreId = carDto.GenreId;
                await carRepo.UpdateCar(getCar);
                           
                return Ok("CAR UPDATE DONE");
            }
            return BadRequest();
        }

    }
}
