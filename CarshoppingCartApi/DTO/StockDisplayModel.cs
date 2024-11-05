namespace CarshoppingCartApi.DTO
{
    public class StockDisplayModel
    {
        public int Id { get; set; }
        public int carId { get; set; }

        public int Quantity { get; set; }

        public string? CarName { get; set; } 
    }
}
