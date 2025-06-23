using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class ReportService : IReportsRepository
    {
        private readonly ApplicationDbContext db;
        public ReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int ExpAlert()
        {
            int expcount = db.Medicines.Where(x => x.ExpiryDate <= DateTime.Now).Count();
            return expcount;
        }

        public List<PurchaseItemDtoSF> ExpAlertTable()
        {
            var expAlertTable = db.PurchaseItems.Include(x => x.Medicine).Where(x => x.Medicine.ExpiryDate.Date <= DateTime.Today).Select(x => new PurchaseItemDtoSF()
            {
                PurchaseItemId = x.PurchaseItemId,
                MedicineId = x.MedicineId,
                MedicineName = x.Medicine.Name,
                BatchNo = x.Medicine.BatchNo,
                ExpiryDate = x.Medicine.ExpiryDate,
                Quantity = x.Quantity,
                CostPrice = x.CostPrice,
            }).ToList();
            return expAlertTable;
        }

        public int PriorExpAlert()
        {
            DateTime Days30 = DateTime.Now + TimeSpan.FromDays(30);
            int priorexpalert = db.Medicines.Where(x => x.ExpiryDate <= Days30 && x.ExpiryDate >= DateTime.Now).Count();
            return priorexpalert;
        }

        public List<PurchaseItemDtoSF> PriorExpAlertTable()
        {
            DateTime Days30 = DateTime.Now + TimeSpan.FromDays(30);
            //db.Medicines.Where(x => x.ExpiryDate <= Days30 && x.ExpiryDate >= DateTime.Now)
            var priorexpalert = db.PurchaseItems.Where(x => x.Medicine.ExpiryDate <= Days30 && x.Medicine.ExpiryDate >= DateTime.Now).Include(x => x.Medicine).Select(x => new PurchaseItemDtoSF()
            {
                PurchaseItemId = x.PurchaseItemId,
                MedicineId = x.MedicineId,
                MedicineName = x.Medicine.Name,
                BatchNo = x.Medicine.BatchNo,
                ExpiryDate = x.Medicine.ExpiryDate,
                Quantity = x.Quantity,
                CostPrice = x.CostPrice,
            }).ToList();
            return priorexpalert;
        }

        public int stockAlert()
        {
            int min = 11;
            int lowStockCount = db.PurchaseItems
                .Where(x => x.Quantity <= min)
                .Count();
            return lowStockCount;
        }

        public List<PurchaseItemDtoSF> stockAlertTable()
        {
            int min = 11;
            var stockAlertTable = db.PurchaseItems.Where(x => x.Quantity <= min).Include(x => x.Medicine).Select(x => new PurchaseItemDtoSF()
            {
                PurchaseItemId = x.PurchaseItemId,
                MedicineId = x.MedicineId,
                MedicineName = x.Medicine.Name,
                BatchNo = x.Medicine.BatchNo,
                ExpiryDate = x.Medicine.ExpiryDate,
                Quantity = x.Quantity,
                CostPrice = x.CostPrice,
            }).ToList();
            return stockAlertTable;
        }

        public List<TodaySaleDto> TodaySalesTable()
        {
            var total_sale = db.SaleItems.Include(x => x.Medicine).Where(x => x.Sale.SaleDate.Date.Equals(DateTime.Today))
                .Select(x => new TodaySaleDto()
                {
                    CustomerName = x.Sale.CustomerName,
                    SaleDate = x.Sale.SaleDate,
                    Quantity = x.Quantity,
                    Discount = x.Discount,
                    TotalAmount = x.Sale.TotalAmount,
                    MedicineName = x.Medicine.Name
                }).ToList();
            return total_sale;
        }

        public List<Top5Dto> Top5()
        {
            var top_sale = db.SaleItems.Include(x => x.Sale).Include(x => x.Medicine).GroupBy(x => x.Medicine.Name).Select(y => new Top5Dto()
            {
                Quantity = y.Sum(x => x.Quantity),
                MedicineName = y.Key

            }).OrderByDescending(x => x.Quantity).Take(5).ToList();
            return top_sale;
        }
    }
}
