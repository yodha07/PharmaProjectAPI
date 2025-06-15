using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo repo;
        public CustomerController(ICustomerRepo repo) 
        {
            this.repo = repo;
        }
        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer(CustomerDTO customer)
        {
            repo.AddCustomer(customer);
            return  Ok("Customer Added Successfully!!");
        }

        [HttpGet]
        [Route("FetchCustomer")]
        public IActionResult GetAllCustomers()
        {
           var data= repo.GetAll();
            return Ok(data);
        }
        [HttpGet]
        [Route("EditCustomer/{id}")]
        public IActionResult EditCustomer(int id)
        {
            var data=repo.GetCustomerById(id);
            return Ok(data);
        }
        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer(CustomerDTO customer)
        {
            repo.UpdateCustomer(customer);
            return Ok("Customer Updated Successfully!!");
        }
        [HttpDelete]
        [Route("DeleteById/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            repo.DeleteCustomer(id);
            return Ok(" Customer Deleted Successfully!!");
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(List<int> ids)
        {
            repo.Delete(ids);
            return Ok("Customers Deleted Successfully");
        }
        
    }
}
