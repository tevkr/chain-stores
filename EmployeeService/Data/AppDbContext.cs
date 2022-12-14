using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt ) : base(opt)
        {
            
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Store>()
                .HasMany(s => s.Employees)
                .WithOne(s=> s.Store!)
                .HasForeignKey(p => p.StoreId);

            modelBuilder
                .Entity<Employee>()
                .HasOne(s => s.Store)
                .WithMany(s => s.Employees)
                .HasForeignKey(s =>s.StoreId);
        }
    }
}