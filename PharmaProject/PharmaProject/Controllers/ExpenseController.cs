using Microsoft.AspNetCore.Mvc;
using PharmaProject.Models;
using Newtonsoft.Json;
using System.Text;

namespace PharmaProject.Controllers
{
    public class ExpenseController : Controller
    {
        HttpClient client;

        public ExpenseController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            client = new HttpClient(clientHandler);
        }

        public IActionResult Index()
        {
            List<ExpenseDTO> data = new List<ExpenseDTO>();
            string url = "https://localhost:7078/api/Expenses/all";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<ExpenseDTO>>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return View(data);
        }

        public IActionResult AddExpense()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddExpense(ExpenseDTO dto)
        {
            dto.Date = DateTime.Now;
            dto.Amount = dto.Amount; // already bound
            string url = "https://localhost:7078/api/Expenses/add";
            var JsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string error = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error: " + error);
            }
            return View(dto);
        }

        public IActionResult Profit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Profit(decimal totalSales, decimal totalCostOfGoods)
        {
            string url = $"https://localhost:7078/api/Expenses/profit?totalSales={totalSales}&totalCostOfGoods={totalCostOfGoods}";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<ProfitDTO>(json);
                return View(obj);
            }
            else
            {
                ViewBag.Error = "Failed to calculate profit.";
            }

            return View();
        }
    }
}
