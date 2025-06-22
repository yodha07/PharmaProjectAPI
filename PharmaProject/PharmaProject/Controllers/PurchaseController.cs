using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaProject.Helper;
using PharmaProject.Models;

namespace PharmaProject.Controllers
{
    public class PurchaseController : Controller
    {
        HttpClient client;

        public PurchaseController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMedicines()
        {
            List<PurchaseMedDTO> data = new List<PurchaseMedDTO>();
            string url = "https://localhost:7078/api/Purchase/GetMed/";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<PurchaseMedDTO>>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }

            return Json(data);
        }

        [HttpPost]
        public IActionResult AddCart(PurchaseCartDTO1 em)
        {
            em.CreatedBy = "Admin";
            string url = "https://localhost:7078/api/Purchase/AddCart/";
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

        public IActionResult GetCart()
        {
            List<PurchaseCartDTO2> data = new List<PurchaseCartDTO2>();
            string url = "https://localhost:7078/api/Purchase/FetchCart/";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<PurchaseCartDTO2>>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }

            return Json(data);
        }

        public IActionResult DeleteSingleCart(int id)
        {
            string url = $"https://localhost:7078/api/Purchase/DeleteSingleCart/{id}";
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

        public IActionResult DeleteCart(List<int> ids)
        {
            string url = "https://localhost:7078/api/Purchase/DeleteCart/";
            var json = JsonConvert.SerializeObject(ids);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
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
        public IActionResult AddPurchase(PurchaseDTO1 em)
        {
            em.CreatedBy = "Admin";
            string url = "https://localhost:7078/api/Purchase/AddPurchase/";
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

        [HttpPost]
        public IActionResult AddPurchaseItem(PurchaseItemDTO1 em)
        {
            em.CreatedBy = "Admin";
            string url = "https://localhost:7078/api/Purchase/AddPurchaseItem/";
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
        public IActionResult GeneratePurchaseInvoicePdf([FromBody] PdfRequestModel model)
        {
            if (model == null || model.CartItems == null || !model.CartItems.Any())
                return BadRequest("Invalid cart data");

            var path = InvoicePdfGenerator.GeneratePurchaseInvoicePdf(model.CartItems, model.Total, model.InvoiceNo);
            return Json(new { success = true, path = path });
        }

    }
}
