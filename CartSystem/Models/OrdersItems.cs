using System.ComponentModel.DataAnnotations;

namespace CartSystem.Models
{
    public class OrdersItems
    {
        [Key]
        public int OrderItemId { get; set; } 
        public string? ProductId { get; set; }
        public decimal Price { get; set; }
        public Guid OrdersId { get; set; }
        public int Quantity { get; set; }
        public virtual Orders? Orders { get; set; }
        public virtual ProductModels? ProductModels { get; set; }
    }
}
