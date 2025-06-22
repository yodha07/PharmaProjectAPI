using Microsoft.AspNetCore.Mvc;
using PharmaProject.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using Humanizer;

namespace PharmaProject.Controllers
{
    public class CustomerController : Controller
    {
        HttpClient client;
        public CustomerController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
        }
        public IActionResult Index()
        {
            List<CustomerDTO> data= new List<CustomerDTO>();
            string url = "https://localhost:7078/api/Customer/FetchCustomer/";
            HttpResponseMessage response=client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            { 
                var json=response.Content.ReadAsStringAsync().Result;
                var obj=JsonConvert.DeserializeObject<List<CustomerDTO>>(json);
                if (obj != null) 
                {
                    data = obj;
                }
            }
            return View(data);
        }
        public IActionResult AddCustomer()
        {
          
            return View();

        }
        [HttpPost]
        public IActionResult AddCustomer(CustomerDTO2 dto)
        {
            dto.CreatedAt = DateTime.Now;
            dto.CreatedBy = "Cashier";
            string url = "https://localhost:7078/api/Customer/AddCustomer/";
            var JsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            {
                string error = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error: " + error);
            }
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public IActionResult DeleteCustomer(int id)
        {
            string url = "https://localhost:7078/api/Customer/DeleteById/";
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        
         public IActionResult UpdateCustomer(int id)
        {
            string url = "https://localhost:7078/api/Customer/EditCustomer/";
            CustomerDTO3 data = new CustomerDTO3();
            HttpResponseMessage response = client.GetAsync(url+id).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<CustomerDTO3>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult UpdatedCustomer(CustomerDTO3 dto)
        {
            dto.ModifiedAt = DateTime.Now;
            dto.ModifiedBy = "Cashier";
            string url = "https://localhost:7078/api/Customer/UpdateCustomer/";
            var JsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
