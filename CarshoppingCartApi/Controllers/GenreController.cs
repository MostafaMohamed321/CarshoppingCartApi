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
    [Authorize(Roles =nameof(Role.Admin))]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepo genreRepo;
        public GenreController(IGenreRepo genreRepo)
        {
            this.genreRepo = genreRepo;
        }
        [HttpPost("AddGenre")]
        public async Task<IActionResult> AddGenre([FromBody]GenreDto genreDto)
        {
            Genre genre = new()
            {
                GenreName= genreDto.GenreName
            };
            if (ModelState.IsValid && genre !=null)
            {
                await genreRepo.AddGenre(genre);
                return Ok("Add Successfully");
            }
            return BadRequest();    
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await genreRepo.GetGenreById(id);
            if (ModelState.IsValid && genre != null) 
            {
                await genreRepo.DeleteGenre(genre);
                return Ok("Deleted Successfully");
            }
            return BadRequest();

        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateGenre(int id,[FromBody]GenreDto genreDto)
        {
            var genre = await genreRepo.GetGenreById(id);
            if (ModelState.IsValid && genre != null)
            {
              genre.GenreName = genreDto.GenreName;
                return Ok("Update Successfully");
            }
            return BadRequest();    
            
        }

    }
}
