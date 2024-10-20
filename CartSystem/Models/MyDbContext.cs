using Microsoft.EntityFrameworkCore;

namespace CartSystem.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            
        }

        public DbSet<ProductModels> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersItems> OrderItems { get; set; }
    }
}
