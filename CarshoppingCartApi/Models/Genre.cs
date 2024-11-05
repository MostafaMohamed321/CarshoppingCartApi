using System.ComponentModel.DataAnnotations;

namespace CarshoppingCartApi.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string GenreName { get; set; }

        public List<Car> Cars { get; set; }
    }
}
