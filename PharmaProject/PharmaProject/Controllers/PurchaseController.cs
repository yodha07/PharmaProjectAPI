using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    }
}
