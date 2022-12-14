using AutoMapper;
using EmployeeService.Data;
using EmployeeService.Dtos;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/e/stores/{storeId}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepo _repository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadDto>> GetEmployeesForStore(int storeId)
        {
            Console.WriteLine($"--> Hit GetEmployeesForStore: {storeId}");

            if (!_repository.StoreExits(storeId))
            {
                return NotFound();
            }

            var employees = _repository.GetEmployeesForStore(storeId);

            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employees));
        }

        [HttpGet("{employeeId}", Name = "GetEmployeeForStore")]
        public ActionResult<EmployeeReadDto> GetEmployeeForStore(int storeId, int employeeId)
        {
            Console.WriteLine($"--> Hit GetEmployeeForStore: {storeId} / {employeeId}");

            if (!_repository.StoreExits(storeId))
            {
                return NotFound();
            }

            var employee = _repository.GetEmployee(storeId, employeeId);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeReadDto>(employee));
        }

        [HttpPost]
        public ActionResult<EmployeeReadDto> CreateEmployeeForStore(int storeId, EmployeeWriteDto employeeDto)
        {
            Console.WriteLine($"--> Hit CreateEmployeeForStore: {storeId}");

            if (!_repository.StoreExits(storeId))
            {
                return NotFound();
            }

            var employee = _mapper.Map<Employee>(employeeDto);

            _repository.CreateEmployee(storeId, employee);
            _repository.SaveChanges();

            var employeeReadDto = _mapper.Map<EmployeeReadDto>(employee);

            return CreatedAtRoute(nameof(GetEmployeeForStore),
                new {storeId = storeId, employeeId = employeeReadDto.Id}, employeeReadDto);
        }

    }
}