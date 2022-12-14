using EmployeeService.Models;
using EmployeeService.SyncDataServices.Grpc;

namespace EmployeeService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IStoreDataClient>();

                var stores = grpcClient.ReturnAllStores();
                if (stores != null)
                    SeedData(serviceScope.ServiceProvider.GetService<IEmployeeRepo>(), stores);
            }
        }
        
        private static void SeedData(IEmployeeRepo repo, IEnumerable<Store> stores)
        {
            Console.WriteLine("Seeding new stores...");
            
            foreach (var store in stores)
            {
                if(!repo.ExternalStoreExists(store.ExternalId))
                {
                    repo.CreateStore(store);
                }
                repo.SaveChanges();
            }
        }
    }
}