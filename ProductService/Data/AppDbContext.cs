using ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt ) : base(opt)
        {
            
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Store>()
                .HasMany(s => s.Products)
                .WithOne(s=> s.Store!)
                .HasForeignKey(p => p.StoreId);

            modelBuilder
                .Entity<Product>()
                .HasOne(s => s.Store)
                .WithMany(s => s.Products)
                .HasForeignKey(s =>s.StoreId);
        }
    }
}