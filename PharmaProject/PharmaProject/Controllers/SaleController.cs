using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaProject.Models;
using System.Net.Http;
using System.Net.Http.Json;

public class SaleController : Controller
{
    private readonly HttpClient _client;

    public SaleController()
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        _client = new HttpClient(handler);
    }

    public IActionResult Index()
    {
        return View(); // Main counter sale UI
    }

    public IActionResult CounterSaleDemo()
    {
        return View(); // Demo/testing view
    }

    [HttpGet]
    public async Task<IActionResult> GetStock()
    {
        try
        {
            string url = "https://localhost:7078/api/medicine/stock";

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<MedicineStockDto>>(json);

                if (data != null && data.Any())
                    return Json(data);
            }

            return Json(new List<MedicineStockDto>());
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ API Error: " + ex.Message);
            return Json(new List<MedicineStockDto>());
        }
    }

    public async Task<IActionResult> StockModal()
    {
        List<MedicineM> data = new();

        try
        {
            string url = "https://localhost:7078/api/medicine/stock";

            var response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<MedicineM>>(json);
                if (result != null)
                    data = result;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Modal API Error: " + ex.Message);
        }

        return View(data);
    }

    [HttpPost("Sale/UpdateStock")]
    public async Task<IActionResult> UpdateStock([FromBody] StockUpdateDto model, [FromQuery] bool add)
    {
        var apiUrl = $"https://localhost:7078/api/Stock/UpdateStock?add={add}";

        try
        {
            var response = await _client.PostAsJsonAsync(apiUrl, model);
            if (response.IsSuccessStatusCode)
            {
                return Ok("Stock updated");
            }

            var error = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, $"Error from API: {error}");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal error: {ex.Message}");
        }
    }

    [HttpPost]
    public IActionResult PreparePrintBill([FromBody] List<BillItemDto> items)
    {
        if (items == null || !items.Any()) return BadRequest("No items selected");
        TempData["PrintBillData"] = JsonConvert.SerializeObject(items);
        return Ok();
    }

    public IActionResult PrintBill()
    {
        var json = TempData["PrintBillData"]?.ToString();
        var items = string.IsNullOrEmpty(json) ? new List<BillItemDto>() : JsonConvert.DeserializeObject<List<BillItemDto>>(json);
        return View(items);
    }



}
