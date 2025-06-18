using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface IDashboard
    {
        Task<int> GetTotalMedicines();
        Task<int> GetTotalSales();
        Task<int> GetExpiringSoon();
        Task<int> GetLowStock();
        Task<List<MonthlySalesDTO>> GetMonthlySales();
        Task<List<TopMedDTO>> GetTopMedicines(int count = 5);
        Task<List<SalesReportDTO>> GetRecentSales(int count = 10);
        Task<List<ExpiredMedicineDTO>> GetExpiredMedicines(int count = 10);
        Task<List<StockSummaryDTO>> GetStockSummaries(int count = 10);

    }
}
