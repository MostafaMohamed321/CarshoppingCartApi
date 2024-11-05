using System.ComponentModel.DataAnnotations;

namespace CarshoppingCartApi.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]

        public int CarId { get; set; }
        [Required]

        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Order Order { get; set; }
        public Car Car { get; set; }
    }
}
