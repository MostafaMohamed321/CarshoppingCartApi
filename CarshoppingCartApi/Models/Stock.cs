namespace CarshoppingCartApi.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int Quantity { get; set; }

        public Car? car { get; set; }
    }
}
