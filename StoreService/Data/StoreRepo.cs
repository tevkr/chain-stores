using StoreService.Models;

namespace StoreService.Data
{
    public class StoreRepo : IStoreRepo
    {
        private readonly AppDbContext _context;

        public StoreRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateStore(Store store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            _context.Stores.Add(store);
        }

        public IEnumerable<Store> GetAllStores()
        {
            return _context.Stores.ToList();
        }

        public Store GetStoreById(int id)
        {
            return _context.Stores.FirstOrDefault(s => s.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}