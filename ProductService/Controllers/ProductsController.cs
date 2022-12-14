using AutoMapper;
using ProductService.Data;
using ProductService.Dtos;
using ProductService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [Route("api/p/stores/{storeId}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetProductsForStore(int storeId)
        {
            Console.WriteLine($"--> Hit GetProductsForStore: {storeId}");

            if (!_repository.StoreExits(storeId))
            {
                return NotFound();
            }

            var products = _repository.GetProductsForStore(storeId);

            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        [HttpGet("{productId}", Name = "GetProductForStore")]
        public ActionResult<ProductReadDto> GetProductForStore(int storeId, int productId)
        {
            Console.WriteLine($"--> Hit GetProductForStore: {storeId} / {productId}");

            if (!_repository.StoreExits(storeId))
            {
                return NotFound();
            }

            var product = _repository.GetProduct(storeId, productId);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductReadDto>(product));
        }

        [HttpPost]
        public ActionResult<ProductReadDto> CreateProductForStore(int storeId, ProductWriteDto productDto)
        {
            Console.WriteLine($"--> Hit CreateProductForStore: {storeId}");

            if (!_repository.StoreExits(storeId))
            {
                return NotFound();
            }

            var product = _mapper.Map<Product>(productDto);

            _repository.CreateProduct(storeId, product);
            _repository.SaveChanges();

            var productReadDto = _mapper.Map<ProductReadDto>(product);

            return CreatedAtRoute(nameof(GetProductForStore),
                new {storeId = storeId, productId = productReadDto.Id}, productReadDto);
        }

    }
}