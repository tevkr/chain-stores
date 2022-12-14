using ProductService.Models;

namespace ProductService.SyncDataServices.Grpc
{
    public interface IStoreDataClient
    {
        IEnumerable<Store> ReturnAllStores();
    }
}