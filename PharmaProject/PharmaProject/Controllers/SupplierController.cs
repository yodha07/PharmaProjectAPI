using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PharmaProject.Models;

namespace PharmaProject.Controllers
{
    public class SupplierController : Controller
    {
        HttpClient client;

        public SupplierController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
        }
        public IActionResult Index()
        {
            List<SupplierDTO2> data = new List<SupplierDTO2>();
            string url = "https://localhost:7078/api/Supplier/FetchSup/";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<SupplierDTO2>>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }

            return View(data);
        }

        public IActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSupplier(SupplierDTO1 em)
        {
            em.CreatedBy = "Admin";
            string url = "https://localhost:7078/api/Supplier/AddSup/";
            var json = JsonConvert.SerializeObject(em);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult EditSupplier(int id)
        {
            SupplierDTO1 data = new SupplierDTO1();
            string url = "https://localhost:7078/api/Supplier/GetSup/";

            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<SupplierDTO1>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult EditSupplier(SupplierDTO1 em)
        {
            em.ModifiedBy = "Admin";
            string url = "https://localhost:7078/api/Supplier/UpdateSup/";
            var json = JsonConvert.SerializeObject(em);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeleteSupplier(int id)
        {
            string url = "https://localhost:7078/api/Supplier/DeleteSup/{id}";
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
