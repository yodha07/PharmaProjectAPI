
//﻿// Controllers/CartController.cs
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using PharmaProject.Models;

//public class CartController : Controller
//{
//    private readonly IHttpClientFactory _httpClientFactory;

//    public CartController(IHttpClientFactory httpClientFactory)
//    {
//        _httpClientFactory = httpClientFactory;
//    }

//    // GET: /Cart
//    public async Task<IActionResult> Index()
//    {
//        var client = _httpClientFactory.CreateClient();
//        var response = await client.GetAsync("https://localhost:7078/api/medicine/cart"); // ✅ correct


//        if (!response.IsSuccessStatusCode)
//        {
//            return View(new List<MedicineCartDTO>());
//        }

//        var json = await response.Content.ReadAsStringAsync();
//        var medicines = JsonConvert.DeserializeObject<List<MedicineCartDTO>>(json);

//        // Default quantity 0
//        foreach (var m in medicines)
//        {
//            m.Quantity = 0;
//        }

//        return View(medicines);
//    }

//    // POST: /Cart
//    [HttpPost]
//    public IActionResult SubmitCart(List<MedicineCartDTO> medicines)
//    {
//        var selectedItems = medicines.Where(m => m.Quantity > 0).ToList();
//        return View("CartSummary", selectedItems); // Show summary
//    }

   
//=======
﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaProject.Models;
using System.Text;

namespace PharmaProject.Controllers
{
    public class CartController : Controller
    {
        HttpClient client;
        public CartController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMedicinesInCard()
        {
       
            List<MedicineCardDTO> data = new List<MedicineCardDTO>();
            string url = "https://localhost:7078/api/Cart/Medicines";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<MedicineCardDTO>>(json);
                if (result != null)
                {
                    data = result;
                }
            }

            return View(data);
        }
    
  
        public IActionResult AddCart()
        {

            return View();

        }

        [HttpPost]
        public IActionResult AddCart(CartDTO1 dto)
        {
          
            string url = "https://localhost:7078/api/Purchase/AddCart/";
            var json = JsonConvert.SerializeObject(dto);
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

    }
}
