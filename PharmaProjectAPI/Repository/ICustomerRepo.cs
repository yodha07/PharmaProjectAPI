using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface ICustomerRepo
    {
        void AddCustomer(CustomerDTO2 customer);
        void UpdateCustomer(CustomerDTO3 customer);
        void DeleteCustomer(int id);
        void Delete(List<int> ids);
        List<Customer> GetAll();
        CustomerDTO3 GetCustomerById(int id);
    }
}
