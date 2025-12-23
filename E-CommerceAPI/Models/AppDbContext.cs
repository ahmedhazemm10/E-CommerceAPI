using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceAPI.Models
{
    public class AppDbContext : IdentityDbContext<User>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItems>()
                .HasIndex(ci => new { ci.CartId, ci.ProductId })
                .IsUnique();
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products {  get; set; }
        public DbSet<Order> Orders {  get; set; }
        public DbSet<OrderItems> OrderItems {  get; set; }
        public DbSet<Cart> Carts {  get; set; }
        public DbSet<CartItems> CartItems {  get; set; }
        public DbSet<Category> Categories {  get; set; }
    }
}
