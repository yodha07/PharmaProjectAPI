using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaProject.Models;
using System.Data;
using System.Net.Security;

namespace PharmaProject.Controllers
{
    public class ReportsController : Controller
    {
        private readonly HttpClient _client;
        public ReportsController()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => true
            };

            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7078")
            };


        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel();
            try
            {
                var stockAlertResp = _client.GetAsync("/api/Report/StockAlert").Result;
                if(stockAlertResp.IsSuccessStatusCode) 
                    model.StockAlertCount = int.Parse(stockAlertResp.Content.ReadAsStringAsync().Result);

                var expAlertResp = _client.GetAsync("/api/Report/ExpAlert").Result;
                if (expAlertResp.IsSuccessStatusCode)
                    model.ExpAlertCount = int.Parse(expAlertResp.Content.ReadAsStringAsync().Result);

                var priorexpAlertResp = _client.GetAsync("/api/Report/PriorExpAlert").Result;
                if (priorexpAlertResp.IsSuccessStatusCode)
                    model.PriorExpAlertCount = int.Parse(priorexpAlertResp.Content.ReadAsStringAsync().Result);

                //Tables

                var stockalertTable = _client.GetAsync("/api/Report/StockAlertTable").Result;
                if (stockalertTable.IsSuccessStatusCode)
                {
                    var json = stockalertTable.Content.ReadAsStringAsync().Result;
                    model.StockAlertTable = JsonConvert.DeserializeObject<List<PurchaseItemDtoSF>>(json);
                }

                var expalertTable = _client.GetAsync("/api/Report/ExpAlertTable").Result;
                if (expalertTable.IsSuccessStatusCode)
                {
                    var json = expalertTable.Content.ReadAsStringAsync().Result;
                    model.ExpAlertTable = JsonConvert.DeserializeObject<List<PurchaseItemDtoSF>>(json);
                }

                var priorexpalertTable = _client.GetAsync("/api/Report/PriorExpAlertTable").Result;
                if (priorexpalertTable.IsSuccessStatusCode)
                {
                    var json = priorexpalertTable.Content.ReadAsStringAsync().Result;
                    model.PriorExpAlertTable = JsonConvert.DeserializeObject<List<PurchaseItemDtoSF>>(json);
                }

                var todaysaleTable = _client.GetAsync("/api/Report/TodaySale").Result;
                if (todaysaleTable.IsSuccessStatusCode)
                {
                    var json = todaysaleTable.Content.ReadAsStringAsync().Result;
                    model.TodaySales = JsonConvert.DeserializeObject<List<TodaySaleDto>>(json);
                }

                var top5Table = _client.GetAsync("/api/Report/Top5").Result;
                if (top5Table.IsSuccessStatusCode)
                {
                    var json = top5Table.Content.ReadAsStringAsync().Result;
                    model.Top5 = JsonConvert.DeserializeObject<List<Top5Dto>>(json);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "API call failed: " + ex.Message;
            }
            return View(model);
        }
    }
}
