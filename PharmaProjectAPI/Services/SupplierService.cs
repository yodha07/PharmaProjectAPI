using AutoMapper;
using Microsoft.OpenApi.Validations;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class SupplierService : ISupplierRepo
    {
        ApplicationDbContext db;
        IMapper mapper;
        public SupplierService(ApplicationDbContext db, IMapper mapper) 
        { 
            this.db = db;
            this.mapper = mapper;
        }


        public void AddSupplier(SupplierDTO supplier)
        {
            var data = mapper.Map<Supplier>(supplier);

            data.CreatedAt = DateTime.Now;
            data.ModifiedAt = DateTime.Now;
            data.ModifiedBy = supplier.CreatedBy;

            db.Suppliers.Add(data);
            db.SaveChanges();
        }
        public List<SupplierDTO2> GetAll() 
        { 
            var data=db.Suppliers.ToList();
            var supplier = mapper.Map<List<SupplierDTO2>>(data);
            return supplier;
        }
        public SupplierDTO GetSupplierById(int id)
        {
            var data = db.Suppliers.Find(id);
            var supplier=mapper.Map<SupplierDTO>(data);
            return supplier;
        }
        public void UpdateSupplier(SupplierDTO supplier) 
        {
            var existing = db.Suppliers.FirstOrDefault(x => x.SupplierId == supplier.SupplierId);
            if (existing != null)
            {
                existing.Name = supplier.Name;
                existing.Contact = supplier.Contact;
                existing.Address = supplier.Address;
                existing.ModifiedAt = DateTime.Now;
                existing.ModifiedBy = supplier.ModifiedBy;

                db.SaveChanges();
            }
        }

        public void DeleteSupplier(int id)
        {
            var data = db.Suppliers.Find(id);
            db.Remove(data);
            db.SaveChanges();
        }
    }
}
