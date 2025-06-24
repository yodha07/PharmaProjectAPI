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

        public void AddCustomer(CustomerDTO2 customer)
        {
            var data = new Customer()
            {
                Name = customer.Name,
                Address = customer.Address,
                EmailId = customer.EmailId,
                Mobile=customer.Mobile,
                CreatedAt = customer.CreatedAt,
                CreatedBy= customer.CreatedBy
                //ModifiedAt= DateTime.Now
                //ModifiedBy="Cashier"
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
        public CustomerDTO3 GetCustomerById(int id)
        {
            var customer= db.Customers.Find(id);
            var data = new CustomerDTO3()
            {
                CustomerId=customer.CustomerId,
                Name = customer.Name,
                Address = customer.Address,
                EmailId = customer.EmailId,
                Mobile = customer.Mobile,
                ModifiedAt = customer.ModifiedAt,
                ModifiedBy = customer.ModifiedBy
                //ModifiedAt= DateTime.Now
                //ModifiedBy="Cashier"
            };
            return data;
        }
        public void UpdateCustomer(CustomerDTO3 customer)
        {
            var existing = db.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
            if (existing != null)
            {
                existing.Name = customer.Name;
                existing.EmailId = customer.EmailId;
                existing.Address = customer.Address;
                existing.Mobile= customer.Mobile;
                existing.ModifiedAt = customer.ModifiedAt;
                existing.ModifiedBy = customer.ModifiedBy;

                db.SaveChanges();
            }


            //var data = new Customer()
            //{
            //    CustomerId = customer.CustomerId,
            //    Name = customer.Name,
            //    EmailId = customer.EmailId,
            //    Address= customer.Address,
            //    Mobile = customer.Mobile,
            //    //CreatedAt = DateTime.Now,
            //    //CreatedBy = "Cashier",
            //    ModifiedAt = DateTime.Now,
            //    ModifiedBy = "Cashier"
            //};
            //db.Customers.Update(data);
            //db.SaveChanges();
        }

        public List<Customer> GetAll()
        {
            return db.Customers.Include(x=>x.Sales).ToList();
        }

        public List<PurchaseHistoryDTO> GetSalesHistory()
        {
            var data = db.Sales
                .Include(x => x.Customer)
                .Include(x => x.SaleItems)
                .ThenInclude(y => y.Medicine)
                .SelectMany(x => x.SaleItems.Select(y => new PurchaseHistoryDTO
                {
                    CustomerName = x.CustomerName,
                    Mobile = x.Customer.Mobile,
                    MedicineName = y.Medicine.Name,
                    TotalAmount = x.TotalAmount,
                    Discount = y.Discount,
                    Quantity = y.Quantity
                }))
                .ToList();

            return data;
        }

    }
}
    