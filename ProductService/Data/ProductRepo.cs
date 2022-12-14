using ProductService.Models;

namespace ProductService.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateProduct(int storeId, Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            product.StoreId = storeId;
            _context.Products.Add(product);
        }

        public void CreateStore(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            _context.Stores.Add(store);
        }

        public bool ExternalStoreExists(int externalStoreId)
        {
            return _context.Stores.Any(s => s.ExternalId == externalStoreId);
        }

        public IEnumerable<Store> GetAllStores()
        {
            return _context.Stores.ToList();
        }

        public Product GetProduct(int storeId, int productId)
        {
            return _context.Products
                .Where(p => p.StoreId == storeId && p.Id == productId).FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsForStore(int storeId)
        {
            return _context.Products
                .Where(p => p.StoreId == storeId)
                .OrderBy(p => p.Store.Name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool StoreExits(int storeId)
        {
            return _context.Stores.Any(s => s.Id == storeId);
        }
    }
}