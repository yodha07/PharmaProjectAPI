using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Migrations;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class DashboardService : IDashboard
    {
        private readonly ApplicationDbContext db;

        private readonly IMapper mapper;

        public DashboardService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> GetTotalMedicines()
        {
            return await db.Medicines.CountAsync();
        }

        public async Task<int> GetExpiringSoon()
        {
            return await db.Medicines.Where(x => x.ExpiryDate <= DateTime.Now.AddDays(30)).CountAsync();
        }

        public async Task<int> GetTotalSales()
        {
            return (int)await db.Sales.SumAsync(x => x.TotalAmount);
        }

        public async Task<int> GetLowStock()
        {
            var medicines = await db.Medicines.Select(x => new {
                MedicineId = x.MedicineId,
                Purchased = db.PurchaseItems.Where(p => p.MedicineId == x.MedicineId)
                            .Sum(p => p.Quantity),
                Sold = db.SaleItems.Where(s => s.MedicineId == x.MedicineId)
                            .Sum(s => s.Quantity)
            }).ToListAsync();

            var lowStockCount = medicines.Count(x => (x.Purchased - x.Sold) < 10);

            return lowStockCount;
        }

        public async Task<List<MonthlySalesDTO>> GetMonthlySales()
        {
            var result = await db.Sales
                .GroupBy(x => new {x.SaleDate.Year, x.SaleDate.Month})
                .Select(g => new MonthlySalesDTO
            {
                Month = g.Key.Month,
                Year = g.Key.Year,
                TotalAmount = g.Sum(s => s.TotalAmount)
            })
            .OrderBy(g => g.Month)
            .ToListAsync();

            return result;
        }

        public async Task<List<TopMedDTO>> GetTopMedicines(int count)
        {
            var topMedicine = await db.SaleItems
                .GroupBy(x => new{ x.MedicineId, x.Medicine.Name, x.Medicine.PricePerUnit})
                .Select(g => new TopMedDTO
            {
                Name = g.Key.Name,
                PricePerUnit = g.Key.PricePerUnit
            })
                .Take(count)
                .ToListAsync();

            return topMedicine;
        }

        public async Task<List<SalesReportDTO>> GetRecentSales(int count = 10)
        {
            var today = DateTime.Today;

            var sales = await db.Sales
                .Where(x => x.SaleDate.Date == today)
                .OrderByDescending(x => x.SaleDate)
                .Take(count)
                .Select(s => new SalesReportDTO
                {
                    SaleId = s.SaleId,
                    CustomerName = s.CustomerName,
                    SaleDate = s.SaleDate,
                    TotalAmount = s.TotalAmount
                }).ToListAsync();

            return sales;
        }

        public async Task<List<ExpiredMedicineDTO>> GetExpiredMedicines(int count = 10)
        {
            var today = DateTime.Today;

            var expired = await db.Medicines
                .Where(x => x.ExpiryDate < today)
                .Select(x => new ExpiredMedicineDTO
                {
                    Name = x.Name,
                    BatchNo = x.BatchNo,
                    ExpiryDate = x.ExpiryDate,
                }).ToListAsync();

            return expired;
        }

        public async Task<List<StockSummaryDTO>> GetStockSummaries(int count = 10)
        {
            var stock = await db.Medicines
                .Select(x => new StockSummaryDTO
                {
                    Name = x.Name,
                    Category = x.Category,
                    Stock = db.PurchaseItems
                            .Where(p => p.MedicineId == x.MedicineId)
                            .Sum(p => p.Quantity) - (db.SaleItems
                                                    .Where(s => s.MedicineId == x.MedicineId)
                                                    .Sum(s => s.Quantity)),
                }).ToListAsync();

            return stock;
        }
    }
}
