using EmployeeService.Models;

namespace EmployeeService.Data
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly AppDbContext _context;
        public EmployeeRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateEmployee(int storeId, Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            employee.StoreId = storeId;
            _context.Employees.Add(employee);
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

        public Employee GetEmployee(int storeId, int employeeId)
        {
            return _context.Employees
                .Where(e => e.StoreId == storeId && e.Id == employeeId).FirstOrDefault();
        }

        public IEnumerable<Employee> GetEmployeesForStore(int storeId)
        {
            return _context.Employees
                .Where(e => e.StoreId == storeId)
                .OrderBy(e => e.Store.Name);
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