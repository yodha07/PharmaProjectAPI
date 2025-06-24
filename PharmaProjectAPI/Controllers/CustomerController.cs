using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
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
        private readonly ApplicationDbContext db;
        public CustomerController(ICustomerRepo repo, ApplicationDbContext db)
        {
            this.repo = repo;
            this.db = db;
        }
        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer(CustomerDTO2 customer)
        {
            repo.AddCustomer(customer);
            return Ok("Customer Added Successfully!!");
        }

        [HttpGet]
        [Route("FetchCustomer")]
        public IActionResult GetAllCustomers()
        {
            var data = repo.GetAll();
            return Ok(data);
        }
        [HttpGet]
        [Route("EditCustomer/{id}")]
        public IActionResult EditCustomer(int id)
        {
            var data = repo.GetCustomerById(id);
            return Ok(data);
        }
        [HttpPut]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer(CustomerDTO3 customer)
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
        [HttpGet]
        [Route("FetchSales")]
        public IActionResult FetchSales()
        {
            var data = repo.GetSalesHistory();
            return Ok(data);
        }

    }
}
