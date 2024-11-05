using System.ComponentModel.DataAnnotations;

namespace CarshoppingCartApi.DTO
{
    public class GenreDto
    {
        [Required]
        [MaxLength(40)]
        public string GenreName { get; set; }

    }
}
