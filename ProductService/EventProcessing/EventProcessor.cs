using System.Text.Json;
using AutoMapper;
using ProductService.Data;
using ProductService.Dtos;
using ProductService.Models;

namespace ProductService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, AutoMapper.IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.StorePublished:
                    addStore(message);
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch(eventType.Event)
            {
                case "Store_Published":
                    Console.WriteLine("--> Store Published Event Detected");
                    return EventType.StorePublished;
                default:
                    Console.WriteLine("--> Could not determine the event type");
                    return EventType.Undetermined;
            }
        }

        private void addStore(string storePublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IProductRepo>();
                
                var storePublishedDto = JsonSerializer.Deserialize<StorePublishedDto>(storePublishedMessage);

                try
                {
                    var store = _mapper.Map<Store>(storePublishedDto);
                    if(!repo.ExternalStoreExists(store.ExternalId))
                    {
                        repo.CreateStore(store);
                        repo.SaveChanges();
                        Console.WriteLine("--> Store added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Store already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Store to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        StorePublished,
        Undetermined
    }
}