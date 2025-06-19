using PharmaProjectAPI.DTO;

namespace PharmaProject.Models
{
    public class Dashboard
    {
        public int TotalMedicines { get; set; }
        public int TotalSales { get; set; }
        public int ExpiringSoon { get; set; }
        public int LowStock { get; set; }

        public List<MonthlySalesDTO>? MonthlySales { get; set; } = new();
        public List<TopMedDTO>? TopMedicines { get; set; } = new();
        public List<SalesReportDTO> TodaySales { get; set; } = new();
        public List<ExpiredMedicineDTO> ExpiredMedicines { get; set; } = new();
        public List<StockSummaryDTO> StockSummaries { get; set; } = new();

    }
}
