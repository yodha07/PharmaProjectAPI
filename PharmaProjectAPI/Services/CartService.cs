using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class CartService:ICartRepository
    {
        private readonly ApplicationDbContext db;
        public CartService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddToCart(Cart cart)
        {
            db.Carts.Add(cart);
            db.SaveChanges();
        }

        public void DeleteCart(int id)
        {
            var data=db.Carts.Find(id);
            db.Carts.Remove(data);
            db.SaveChanges();
        }

        public List<CartDTO2> GetCartItems(int id)
        {
            return db.Carts
                .Include(c => c.Medicine)
                .Include(c => c.User)
                .Where(c => c.UserId == id)
                .Select(c => new CartDTO2()
                {
                    CartId = c.CartId,
                    userName = c.User.Username,
                    medicineName = c.Medicine.Name,
                    Quantity = c.Quantity,

                    // ✅ Get latest CostPrice from PurchaseItems for the same medicine
                    Price = db.PurchaseItems
                             .Where(p => p.MedicineId == c.MedicineId)
                             .OrderByDescending(p => p.CreatedAt)
                             .Select(p => p.CostPrice)
                             .FirstOrDefault()
                })
                .ToList();
        }


//=======
//        public List<MedicineCardDTO> GetAllAvailableMedicineCards()
//        {
//            var medicineIds = db.PurchaseItems
//                .Select(p => p.MedicineId)
//                .Distinct()
//                .ToList();

//            var medicines = db.Medicines
//                .Where(m => medicineIds.Contains(m.MedicineId))
//                .Select(m => new MedicineCardDTO
//                {
//                    MedicineId = m.MedicineId,
//                    Name = m.Name,
//                    Category = m.Category,
//                    Manufacturer = m.Manufacturer,
//                    PricePerUnit = m.PricePerUnit,
//                    BatchNo = m.BatchNo,
//                    ExpiryDate = m.ExpiryDate
//                }).ToList();

//            return medicines;
//        }

//    }
}
