﻿using System.Text;
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
            return View();
        }
        public IActionResult GetAllSupplier()
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

            return Json(data);
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
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public IActionResult GetSupplierById(int id)
        {
            SupplierDTO3 data = new SupplierDTO3();
            string url = "https://localhost:7078/api/Supplier/GetSup/";

            HttpResponseMessage response = client.GetAsync(url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<SupplierDTO3>(json);
                if (obj != null)
                {
                    data = obj;
                }
            }
            return Json(data);
        }

        [HttpPost]
        public IActionResult EditSupplier(SupplierDTO3 em)
        {
            em.ModifiedBy = "Admin";
            string url = "https://localhost:7078/api/Supplier/UpdateSup/";
            var json = JsonConvert.SerializeObject(em);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        public IActionResult DeleteSupplier(int id)
        {
            string url = $"https://localhost:7078/api/Supplier/DeleteSup/{id}";
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
