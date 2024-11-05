using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarshoppingCartApi.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string? CarName { get; set; }
   
        public double Price { get; set; }
        public string? Image { get; set; }

        [Required]

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public List<OrderDetails> OrderDetail { get; set; }

        public List<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }
        [NotMapped]
        public string? GenreName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }

    }
}
