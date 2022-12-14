using EmployeeService.Models;

namespace EmployeeService.SyncDataServices.Grpc
{
    public interface IStoreDataClient
    {
        IEnumerable<Store> ReturnAllStores();
    }
}