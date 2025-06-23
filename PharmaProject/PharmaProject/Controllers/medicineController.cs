using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using PharmaProject.Models;

namespace PharmaProject.Controllers
{
    public class medicineController : Controller
    {
        HttpClient client;

        public medicineController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
        }
        public IActionResult Index()
        {
            List<MedicineM> data = new List<MedicineM>();
            string url = "https://localhost:7078/api/medicine/fetch";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jason = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<MedicineM>>(jason);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return View(data);
        }

        public IActionResult Add()
        {
            List<MedicineM> data = new List<MedicineM>();
            string url = "https://localhost:7078/api/medicine/fetch";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jason = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<MedicineM>>(jason);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return View(new MedicineM());

        }
        [HttpPost]

        public IActionResult Add(MedicineM m)
        {
            string url = "https://localhost:7078/api/medicine/Add";
            var jason = JsonConvert.SerializeObject(m);
            StringContent content = new StringContent(jason, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }
        
        public IActionResult Delete(int id)
        {
           
            string url = "https://localhost:7078/api/medicine/Delete/";
            HttpResponseMessage response = client.DeleteAsync(url+id).Result;
            if(response.IsSuccessStatusCode)
            {
                    return RedirectToAction("Index");
                //var jason = response.Content.ReadAsStringAsync().Result;
                //var obj = JsonConvert.DeserializeObject<MedicineM>(jason);
                //if (obj != null)
                //{
                //    data = obj;
                //}
                //return View(data);
            }
            else
            {
                return View();
            }


               
        }
        //[HttpPost]
        //public IActionResult DeleteC(MedicineM m)
        //{
            
        //    string url = $"https://localhost:7078/api/medicine/Delete/{m.medicineId}";

        //    HttpResponseMessage response = client.DeleteAsync(url).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        [HttpGet]
        public IActionResult Edit(int id)
        {
            MedicineM data = new MedicineM();
            string url = "https://localhost:7078/api/medicine/EditById/";

            HttpResponseMessage response = client.GetAsync(url+id).Result;
            if (response.IsSuccessStatusCode)
            {
                var jason = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<MedicineM>(jason);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return View(data);
        
        }
        [HttpPost]
        public IActionResult Edit(MedicineM m)
        {

            string url = "https://localhost:7078/api/medicine/Edit/";
            var jason = JsonConvert.SerializeObject(m);
            StringContent content = new StringContent(jason, Encoding.UTF8, "application/json");

            HttpResponseMessage response =  client.PutAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Card()
        {
            List<MedicineM> data = new List<MedicineM>();
            string url = "https://localhost:7078/api/medicine/fetch";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jason = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<MedicineM>>(jason);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return View(data);
        }


    }
}
