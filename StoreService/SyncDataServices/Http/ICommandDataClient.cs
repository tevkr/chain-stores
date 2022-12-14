using System.Threading.Tasks;
using StoreService.Dtos;

namespace StoreService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendStoreToEmployee(StoreReadDto store); 
        Task SendStoreToProduct(StoreReadDto store); 
    }
}