// Controllers/CartController.cs
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaProject.Models;

public class CartController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CartController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET: /Cart
    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:7078/api/medicine/cart"); // ✅ correct


        if (!response.IsSuccessStatusCode)
        {
            return View(new List<MedicineCartDTO>());
        }

        var json = await response.Content.ReadAsStringAsync();
        var medicines = JsonConvert.DeserializeObject<List<MedicineCartDTO>>(json);

        // Default quantity 0
        foreach (var m in medicines)
        {
            m.Quantity = 0;
        }

        return View(medicines);
    }

    // POST: /Cart
    [HttpPost]
    public IActionResult SubmitCart(List<MedicineCartDTO> medicines)
    {
        var selectedItems = medicines.Where(m => m.Quantity > 0).ToList();
        return View("CartSummary", selectedItems); // Show summary
    }

   
}
