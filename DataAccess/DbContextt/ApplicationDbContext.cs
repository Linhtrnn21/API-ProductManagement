using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PM.Entities;

namespace PM.DbContextt
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1", Price = 100.00m, Quantity = 10, Description = "Description for Product 1" },
                new Product { Id = 2, Name = "Product 2", Price = 200.00m, Quantity = 5, Description = "Description for Product 2" },
                new Product { Id = 3, Name = "Product 3", Price = 300.00m, Quantity = 15, Description = "Description for Product 3" },
                new Product { Id = 4, Name = "Product 4", Price = 150.00m, Quantity = 20, Description = "Description for Product 4" },
                new Product { Id = 5, Name = "Product 5", Price = 250.00m, Quantity = 8, Description = "Description for Product 5" },
                new Product { Id = 6, Name = "Product 6", Price = 180.00m, Quantity = 12, Description = "Description for Product 6" },
                new Product { Id = 7, Name = "Product 7", Price = 220.00m, Quantity = 18, Description = "Description for Product 7" },
                new Product { Id = 8, Name = "Product 8", Price = 90.00m, Quantity = 25, Description = "Description for Product 8" },
                new Product { Id = 9, Name = "Product 9", Price = 300.00m, Quantity = 30, Description = "Description for Product 9" },
                new Product { Id = 10, Name = "Product 10", Price = 400.00m, Quantity = 2, Description = "Description for Product 10" }
            );
        }
    }
}
