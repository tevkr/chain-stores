using EmployeeService.Models;

namespace EmployeeService.Data
{
    public interface IEmployeeRepo
    {
        bool SaveChanges();

        // Stores
        IEnumerable<Store> GetAllStores();
        void CreateStore(Store store);
        bool StoreExits(int storeId);
        bool ExternalStoreExists(int externalStoreId);

        // Employee
        IEnumerable<Employee> GetEmployeesForStore(int storeId);
        Employee GetEmployee(int storeId, int employeeId);
        void CreateEmployee(int storeId, Employee employee);
    }
}