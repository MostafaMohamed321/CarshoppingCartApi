using CarshoppingCartApi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarshoppingCartApi.DTO
{
    public class CarDto
    {
      

        [Required]
        [MaxLength(40)]
        public string? CarName { get; set; }

        public double Price { get; set; }
       // public string? Image { get; set; }
        //[Required]
        public IFormFile? ImageFile {  get; set; }

        [Required]

        public int GenreId { get; set; }
       
    }
}
