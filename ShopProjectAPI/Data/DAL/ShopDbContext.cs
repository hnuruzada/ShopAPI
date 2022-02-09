using Microsoft.EntityFrameworkCore;
using ShopProjectAPI.Data.Entity;

namespace ShopProjectAPI.Data.DAL
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
