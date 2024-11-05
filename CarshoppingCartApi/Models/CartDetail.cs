using System.ComponentModel.DataAnnotations;

namespace CarshoppingCartApi.Models
{
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int CarId { get; set; }
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Car Car { get; set; }
        public ShoppingCart shoppingCart { get; set; }

    }
}
