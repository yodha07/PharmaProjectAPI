using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using PharmaProjectAPI.DTO;
using PharmaProject.Filters;
using Microsoft.AspNetCore.Authorization;

namespace PharmaProject.Controllers
{
    [GlobalException]
    public class DashboardController : Controller
    {
        HttpClient client;
        public DashboardController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true;
            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri("https://localhost:7078/api/Dashboard/");
        }

        [Authorize(Roles = "Admin,Pharmacist, Cashier")]
        public async Task<IActionResult> Index()
        {
            var model = new Models.Dashboard();

            var medRes = await client.GetAsync("GetTotalMedicines");
            if (medRes.IsSuccessStatusCode)
            {
                model.TotalMedicines = int.Parse(await medRes.Content.ReadAsStringAsync());
            }

            var salesRes = await client.GetAsync("GetTotalSales");
            if (salesRes.IsSuccessStatusCode)
            {
                model.TotalSales = int.Parse(await salesRes.Content.ReadAsStringAsync());
            }

            var expRes = await client.GetAsync("GetExpiringSoon");
            if (expRes.IsSuccessStatusCode)
            {
                model.ExpiringSoon = int.Parse(await expRes.Content.ReadAsStringAsync());
            }

            var lowStockRes = await client.GetAsync("GetLowStock");
            if (lowStockRes.IsSuccessStatusCode)
            {
                model.LowStock = int.Parse(await lowStockRes.Content.ReadAsStringAsync());
            }

            var monthlySalesRes = await client.GetAsync("GetMonthlySales");
            if (monthlySalesRes.IsSuccessStatusCode)
            {
                var json = await monthlySalesRes.Content.ReadAsStringAsync();
                model.MonthlySales = JsonConvert.DeserializeObject<List<MonthlySalesDTO>>(json);
            }

            var topMedsRes = await client.GetAsync("GetTopMedicines");
            if (topMedsRes.IsSuccessStatusCode)
            {
                var json = await topMedsRes.Content.ReadAsStringAsync();
                model.TopMedicines = JsonConvert.DeserializeObject<List<TopMedDTO>>(json);
            }

            return View(model);
        }

        public async Task<IActionResult> ReportView()
        {
            var model = new Models.Dashboard();

            var res1 = await client.GetAsync("GetRecentSales");
            if (res1.IsSuccessStatusCode)
            {
                var json = await res1.Content.ReadAsStringAsync();
                model.TodaySales = JsonConvert.DeserializeObject<List<SalesReportDTO>>(json);
            }

            var res2 = await client.GetAsync("GetExpiredMedicines");
            if (res2.IsSuccessStatusCode)
            {
                var json = await res2.Content.ReadAsStringAsync();
                model.ExpiredMedicines = JsonConvert.DeserializeObject<List<ExpiredMedicineDTO>>(json);
            }

            var res3 = await client.GetAsync("GetStockSummaries");
            if (res3.IsSuccessStatusCode)
            {
                var json = await res3.Content.ReadAsStringAsync();
                model.StockSummaries = JsonConvert.DeserializeObject<List<StockSummaryDTO>>(json);
            }
            return View(model);

        }
    }
}
