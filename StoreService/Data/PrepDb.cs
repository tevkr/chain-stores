using Microsoft.EntityFrameworkCore;
using StoreService.Models;

namespace StoreService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if(isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }
            
            if(!context.Stores.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Stores.AddRange(
                    new Store() {Name="Шестерочка", Address="Комсомольский проспект, 15"},
                    new Store() {Name="Переход", Address="улица Гоголя, 10"},
                    new Store() {Name="Зилла", Address="Косинская улица, 9с21"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}