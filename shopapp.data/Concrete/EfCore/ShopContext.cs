using Microsoft.EntityFrameworkCore;
using shopapp.data.Configurations;
using shopapp.entity;


namespace shopapp.data.Concrete.EfCore
{
    public class ShopContext: DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {          
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ProductCategoryConfiguration());

            builder.Seed();
        }   
    }
}