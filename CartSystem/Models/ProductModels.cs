namespace CartSystem.Models
{
    public class ProductModels
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
