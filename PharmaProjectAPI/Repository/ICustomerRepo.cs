using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface ICustomerRepo
    {
        void AddCustomer(CustomerDTO customer);
        void UpdateCustomer(CustomerDTO customer);
        void DeleteCustomer(int id);
        void Delete(List<int> ids);
        List<Customer> GetAll();
        Customer GetCustomerById(int id);
    }
}
