using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class StockService : IStockRepo
    {
        private readonly ApplicationDbContext db;

        public StockService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool AdjustStock(StockUpdateDTO dto, bool add)
        {
            var name = dto.Name.ToLower().Trim();
            var batch = dto.BatchNo.ToLower().Trim();

            var medicine = db.Medicines.FirstOrDefault(m =>
                m.Name.ToLower().Trim() == name &&
                m.BatchNo.ToLower().Trim() == batch);

            if (medicine == null)
            {
                Console.WriteLine($"❌ Medicine not found: {dto.Name}, {dto.BatchNo}");
                return false;
            }

            var purchaseItem = db.PurchaseItems.FirstOrDefault(p => p.MedicineId == medicine.MedicineId);
            if (purchaseItem == null)
            {
                Console.WriteLine($"❌ PurchaseItem not found for MedicineId: {medicine.MedicineId}");
                return false;
            }

            Console.WriteLine($"{(add ? "➕" : "➖")} {dto.Quantity} to stock for {dto.Name}, {dto.BatchNo}");

            if (add)
                purchaseItem.Quantity += dto.Quantity;
            else
                purchaseItem.Quantity = Math.Max(0, purchaseItem.Quantity - dto.Quantity);

            db.SaveChanges();
            return true;
        }
        }


    }
