using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaProject.Models;
using System.Text;

namespace PharmaProject.Controllers
{
    public class OSaleController : Controller
    {
        HttpClient client;
        public OSaleController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSale(OSaleDTO1 em)
        {
            em.CreatedBy = "Cashier";
            string url = "https://localhost:7078/api/OSale/AddSale/";
            var json = JsonConvert.SerializeObject(em);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public IActionResult AddSaleItem(OSaleItemDTO1 em)
        {
            em.CreatedBy = "Cashier";
            string url = "https://localhost:7078/api/OSale/AddSaleItem/";
            var json = JsonConvert.SerializeObject(em);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public IActionResult GetSale()
        {
            List<OSaleDTO2> data = new List<OSaleDTO2>();
            string url = "https://localhost:7078/api/OSale/FetchSale/";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<OSaleDTO2>>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }

            return Json(data);
        }

        public IActionResult GetSaleItemById(int id)
        {
            List<OSaleItemDTO2> data = new List<OSaleItemDTO2>();
            string url = "https://localhost:7078/api/OSale/FetchSaleItem/";

            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<OSaleItemDTO2>>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return Json(data);
        }

        public IActionResult DeleteSale(int id)
        {
            string url = $"https://localhost:7078/api/OSale/DeleteSale/{id}";
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public IActionResult DeleteSaleItem(int id)
        {
            string url = $"https://localhost:7078/api/OSale/DeleteSaleItem/{id}";
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
