using ProductService.Models;

namespace ProductService.Data
{
    public interface IProductRepo
    {
        bool SaveChanges();

        // Stores
        IEnumerable<Store> GetAllStores();
        void CreateStore(Store store);
        bool StoreExits(int storeId);
        bool ExternalStoreExists(int externalStoreId);

        // Product
        IEnumerable<Product> GetProductsForStore(int storeId);
        Product GetProduct(int storeId, int productId);
        void CreateProduct(int storeId, Product product);
    }
}