using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaProject.Models;

namespace PharmaProject.Controllers
{
    public class PurchaseItemController : Controller
    {
        HttpClient client;

        public PurchaseItemController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetPurchase()
        {
            List<PurchaseDTO2> data = new List<PurchaseDTO2>();
            string url = "https://localhost:7078/api/Purchase/FetchPurchase/";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<PurchaseDTO2>>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }

            return Json(data);
        }

        public IActionResult GetPurchaseItemById(int id)
        {
            List<PurchaseItemDTO2> data = new List<PurchaseItemDTO2>();
            string url = "https://localhost:7078/api/Purchase/FetchPurchaseItem/";

            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<PurchaseItemDTO2>>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return Json(data);
        }

        public IActionResult DeletePurchase(int id)
        {
            string url = $"https://localhost:7078/api/Purchase/DeletePurchase/{id}";
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

        public IActionResult DeletePurchaseItem(int id)
        {
            string url = $"https://localhost:7078/api/Purchase/DeletePurchaseItem/{id}";
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
