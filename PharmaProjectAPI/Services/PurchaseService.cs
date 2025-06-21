using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class PurchaseService : IPurchaseRepo
    {
        ApplicationDbContext db;
        IMapper mapper;
        public PurchaseService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public List<PurchaseMedDTO> GetMedicines()
        {
            var data=db.Medicines.ToList();
            var medicine=mapper.Map<List<PurchaseMedDTO>>(data);
            return medicine;
        }

        public void AddCart(PurchaseCartDTO cart)
        {
            var data = mapper.Map<PurchaseCart>(cart);
            data.CreatedAt = DateTime.Now;
            db.PurchaseCarts.Add(data);
            db.SaveChanges();
        }
        public List<PurchaseCartDTO2> GetAll() 
        { 
            var data=db.PurchaseCarts.Include(x=>x.Medicine).Include(x => x.Supplier).ToList();
            var cart = mapper.Map<List<PurchaseCartDTO2>>(data);
            return cart;
        }

        public void DeleteSingleCart(int id)
        {
            var data = db.PurchaseCarts.Find(id);
            db.PurchaseCarts.RemoveRange(data);
            db.SaveChanges();
        }
        public void DeleteCart(List<int> ids)
        {
            foreach (var id in ids)
            {
                var data = db.PurchaseCarts.Find(id);
                if (data != null)
                {
                    db.PurchaseCarts.RemoveRange(data);
                }
            }
            db.SaveChanges();
        }

        public void AddPurchase(PurchaseDTO purchase)
        { 
            var data=mapper.Map<Purchase>(purchase);
            data.CreatedAt = DateTime.Now;
            db.Purchases.Add(data);
            db.SaveChanges();
        }
        public List<PurchaseDTO2> GetPurchase()
        {
            var data = db.Purchases.Include(x => x.Supplier).ToList();
            var purchase = mapper.Map<List<PurchaseDTO2>>(data);
            return purchase;
        }

        public int DeletePurchase(int id)
        { 
            var purchase = db.Purchases.Include(x => x.PurchaseItems).FirstOrDefault(x => x.PurchaseId == id);
            if (purchase.PurchaseItems.Count > 0)
            {
                return 0;
            }
            else
            {
                db.Purchases.Remove(purchase);
                db.SaveChanges();
                return 1;
            }
        }

        public void AddPurchaseItem(PurchaseItemDTO purchaseitem)
        {
            var id=db.Purchases.OrderByDescending(x=>x.PurchaseId).Select(x=>x.PurchaseId).FirstOrDefault();
            var data = mapper.Map<PurchaseItem>(purchaseitem);
            data.PurchaseId = int.Parse((id).ToString());
            data.CreatedAt = DateTime.Now;
            db.PurchaseItems.Add(data);
            db.SaveChanges();
        }
        public List<PurchaseItemDTO2> GetPurchaseItem(int id)
        {
            var data = db.PurchaseItems.Include(x => x.Medicine).Include(x => x.Purchase).Where(x => x.PurchaseId==id).ToList();
            var purchase = mapper.Map<List<PurchaseItemDTO2>>(data);
            return purchase;
        }
        public int DeletePurchaseItem(int id)
        {
            var purchaseitem = db.PurchaseItems.Include(x => x.SaleItems).FirstOrDefault(x => x.PurchaseItemId == id);
            if (purchaseitem.SaleItems.Count > 0)
            {
                return 0;
            }
            else
            {
                db.PurchaseItems.Remove(purchaseitem);
                db.SaveChanges();
                return 1;
            }
        }
    }
}
