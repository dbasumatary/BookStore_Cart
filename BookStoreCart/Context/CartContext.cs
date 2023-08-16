using BookStoreCart.Entity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreCart.Context
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options) { }

        public DbSet<CartEntity> Cart { get; set; }
    }
}
