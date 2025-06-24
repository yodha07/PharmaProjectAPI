using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class MedicineService : IMedicine
    {
        private readonly ApplicationDbContext db;
        public MedicineService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void AddMedi(MedicineDTO m)
        {
            var s = new Medicine()
            {
                MedicineId = m.MedicineId,
                Name = m.Name,
                Category = m.Category,
                Manufacturer = m.Manufacturer,
                PricePerUnit = m.PricePerUnit,
                BatchNo = m.BatchNo,
                ExpiryDate = m.ExpiryDate,  
                CreatedAt = m.CreatedAt,
                CreatedBy = m.CreatedBy,
                ModifiedAt = m.ModifiedAt,
                ModifiedBy = m.ModifiedBy,
            };
            db.Medicines.Add(s);
            db.SaveChanges();

        }
        
        public void Delete(int id)
        {
            var y = db.Medicines.Find(id);
            if(y!=null)
            {
                db.Medicines.Remove(y);
                db.SaveChanges();
            }
        }
        public List<Medicine> fetch()
        {
            return db.Medicines.ToList();


        }
        public Medicine GetMedicineId(int id)
        {
         var y= db.Medicines.Find(id);
           return new Medicine
            {
               MedicineId=y.MedicineId,
               Name=y.Name,
               Category = y.Category,
               Manufacturer = y.Manufacturer,
               PricePerUnit = y.PricePerUnit,
               BatchNo = y.BatchNo,
               ExpiryDate = y.ExpiryDate,
               CreatedAt = y.CreatedAt,
               CreatedBy = y.CreatedBy,
               ModifiedAt = y.ModifiedAt,
               ModifiedBy = y.ModifiedBy,

            };
        }
        //public void edit(Medicine m)
        //{
        //    db.Medicines.Update(m);
        //    db.SaveChanges();
        //}
 
        public void Edit(MedicineEdit edit)
        {
            var medicine = new Medicine
            {
                MedicineId=edit.MedicineId,
                Name=edit.Name,
                Category=edit.Category,
                Manufacturer=edit.Manufacturer,
                PricePerUnit=edit.PricePerUnit,
                BatchNo=edit.BatchNo,
                ExpiryDate=edit.ExpiryDate,
                ModifiedAt=edit.ModifiedAt,
                ModifiedBy=edit.ModifiedBy
            };
            //if(edit==null)
            //{
            //    throw new Exception("edit null");
            //}
            //var f=db.Medicines.Find(id);

            //if (f == null)
            //{
            //    throw new Exception("medi no found");
            //}
                //f.MedicineId = edit.MedicineId;
                //f.Name = edit.Name;
                //f.Category = edit.Category;
                //f.Manufacturer = edit.Manufacturer;
                //f.PricePerUnit = edit.PricePerUnit;
                //f.BatchNo = edit.BatchNo;
                //f.ExpiryDate = edit.ExpiryDate;
                //f.CreatedAt = edit.CreatedAt;
                //f.CreatedBy = edit.CreatedBy;
                //f.ModifiedAt = edit.ModifiedAt;
                //f.ModifiedBy = edit.ModifiedBy;
            
      
            db.Medicines.Update(medicine);
            db.SaveChanges();
        }
        public List<MedicineStockDto> GetMedicineStock()
        {
            var stockData = db.PurchaseItems
                .GroupBy(pi => pi.MedicineId)
                .Select(group => new
                {
                    MedicineId = group.Key,
                    TotalQuantity = group.Sum(x => x.Quantity),
                    CostPrice = group.First().CostPrice
                })
                .Join(db.Medicines,
                      g => g.MedicineId,
                      m => m.MedicineId,
                      (g, m) => new MedicineStockDto
                      {
                          MedicineId = m.MedicineId,
                          Name = m.Name,
                          Category = m.Category,
                          BatchNo = m.BatchNo,
                          ExpiryDate = m.ExpiryDate,
                          TotalQuantity = g.TotalQuantity,
                          CostPrice = g.CostPrice
                      })
                .ToList();

            return stockData;
        }

        public List<MedicineCartDTO> GetMedicinesForCart()
        {
            return db.Medicines
                .Select(m => new MedicineCartDTO
                {
                    MedicineId = m.MedicineId,
                    Name = m.Name,
                    Category = m.Category,

                    // ✅ Get latest cost price from PurchaseItems table
                    Price = db.PurchaseItems
                        .Where(p => p.MedicineId == m.MedicineId)
                        .OrderByDescending(p => p.CreatedAt)
                        .Select(p => p.CostPrice)
                        .FirstOrDefault()
                })
                .ToList();
        }



    }
}
