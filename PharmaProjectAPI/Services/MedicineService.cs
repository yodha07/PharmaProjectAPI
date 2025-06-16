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
        public MedicineEdit GetMedicineId(int id)
        {
         var y= db.Medicines.Find(id);
           return new MedicineEdit
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
        public List<Medicine> fetch()
        {
          return db.Medicines.ToList();
       
            
        }
        public void Edit(int id,MedicineEdit edit)
        {
            if(edit==null)
            {
                throw new Exception("edit null");
            }
            var f=db.Medicines.Find(id);

            if (f == null)
            {
                throw new Exception("medi no found");
            }
                //f.MedicineId = edit.MedicineId;
                f.Name = edit.Name;
                f.Category = edit.Category;
                f.Manufacturer = edit.Manufacturer;
                f.PricePerUnit = edit.PricePerUnit;
                f.BatchNo = edit.BatchNo;
                f.ExpiryDate = edit.ExpiryDate;
                f.CreatedAt = edit.CreatedAt;
                f.CreatedBy = edit.CreatedBy;
                f.ModifiedAt = edit.ModifiedAt;
                f.ModifiedBy = edit.ModifiedBy;
            
      
            db.Medicines.Update(f);
            db.SaveChanges();
        }
    }
}
