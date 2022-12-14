using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreService.AsyncDataServices;
using StoreService.Data;
using StoreService.Dtos;
using StoreService.Models;
using StoreService.SyncDataServices.Http;

namespace StoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public StoresController(
            IStoreRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StoreReadDto>> GetStores()
        {
            Console.WriteLine("--> Getting Stores....");

            var storeItem = _repository.GetAllStores();

            return Ok(_mapper.Map<IEnumerable<StoreReadDto>>(storeItem));
        }

        [HttpGet("{id}", Name = "GetStoreById")]
        public ActionResult<StoreReadDto> GetStoreById(int id)
        {
            var storeItem = _repository.GetStoreById(id);
            if (storeItem != null)
            {
                return Ok(_mapper.Map<StoreReadDto>(storeItem));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<StoreReadDto>> CreateStore(StoreWriteDto storeWriteDto)
        {
            var storeModel = _mapper.Map<Store>(storeWriteDto);
            _repository.CreateStore(storeModel);
            _repository.SaveChanges();

            var storeReadDto = _mapper.Map<StoreReadDto>(storeModel);

            // Send Sync Message
            try
            {
                await _commandDataClient.SendStoreToEmployee(storeReadDto);
                await _commandDataClient.SendStoreToProduct(storeReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
            }

            //Send Async Message
            try
            {
                var storePublishedDto = _mapper.Map<StorePublishedDto>(storeReadDto);
                storePublishedDto.Event = "Store_Published";
                _messageBusClient.PublishNewStore(storePublishedDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetStoreById), new { Id = storeReadDto.Id}, storeReadDto);
        }
    }
}