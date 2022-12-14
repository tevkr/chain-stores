using Microsoft.EntityFrameworkCore;
using StoreService.Models;

namespace StoreService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Store> Stores { get; set; }
    }
}