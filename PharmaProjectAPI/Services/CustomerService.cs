using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class CustomerService:ICustomerRepo
    {
        private readonly ApplicationDbContext db;
        public CustomerService(ApplicationDbContext db)
        { 
            this.db = db;
        }

        public void AddCustomer(CustomerDTO customer)
        {
            var data = new Customer()
            {
               
                Name = customer.Name,
                EmailId = customer.EmailId,
                Mobile=customer.Mobile,
                CreatedAt = DateTime.Now,
                CreatedBy="Cashier",
                ModifiedAt= DateTime.Now,
                ModifiedBy="Cashier"
            };
            db.Customers.Add(data);
            db.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var data = db.Customers.Find(id);
            db.Customers.Remove(data);
            db.SaveChanges();
            
        }
        public void Delete(List<int> ids)
        {
            foreach(var id in ids)
            {
                var data = db.Customers.Find(id);
                if (data != null)
                {
                    db.Customers.RemoveRange(data);
                }
             }
            db.SaveChanges();
        }
        public Customer GetCustomerById(int id)
        {
            return db.Customers.Find(id);
          
        }
        public void UpdateCustomer(CustomerDTO customer)
        {
            var data = new Customer()
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                EmailId = customer.EmailId,
                Mobile = customer.Mobile,
                CreatedAt = DateTime.Now,
                CreatedBy = "Cashier",
                ModifiedAt = DateTime.Now,
                ModifiedBy = "Cashier"
            };
            db.Customers.Update(data);
            db.SaveChanges();
        }

        public List<Customer> GetAll()
        {
            return db.Customers.Include(x=>x.Sales).ToList();
        }
    }
}
    