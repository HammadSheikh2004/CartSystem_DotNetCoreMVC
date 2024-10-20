using System.ComponentModel.DataAnnotations;

namespace CartSystem.Models
{
    public class Orders
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public virtual ICollection<OrdersItems>? OrderData { get; set; }
    }
}
