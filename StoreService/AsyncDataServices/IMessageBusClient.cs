using StoreService.Dtos;

namespace StoreService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewStore(StorePublishedDto storePublishedDto);
    }
}