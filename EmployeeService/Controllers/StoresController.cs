using AutoMapper;
using EmployeeService.Data;
using EmployeeService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/e/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IEmployeeRepo _repository;
        private readonly IMapper _mapper;

        public StoresController(IEmployeeRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StoreReadDto>> GetStores()
        {
            Console.WriteLine("--> Getting Stores from EmployeeService");

            var storeItems = _repository.GetAllStores();

            return Ok(_mapper.Map<IEnumerable<StoreReadDto>>(storeItems));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Employee Service");

            return Ok("Inbound test of from Stores Controler");
        }
    }
}