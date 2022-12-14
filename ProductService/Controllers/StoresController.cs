using AutoMapper;
using ProductService.Data;
using ProductService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [Route("api/p/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;

        public StoresController(IProductRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StoreReadDto>> GetStores()
        {
            Console.WriteLine("--> Getting Stores from ProductService");

            var storeItems = _repository.GetAllStores();

            return Ok(_mapper.Map<IEnumerable<StoreReadDto>>(storeItems));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Product Service");

            return Ok("Inbound test of from Stores Controler");
        }
    }
}