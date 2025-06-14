using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaProject.Models;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Text.Unicode;

namespace PharmaProject.Controllers
{
    public class AuthController : Controller
    {
        HttpClient client;

        public AuthController()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true;
            client = new HttpClient(clientHandler);
        }

        public IActionResult Index()
        {
            List <UsersDTO> users = new List<UsersDTO>();
            string url = "https://localhost:7078/api/Auth/GetUser";

            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsondata = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<List<UsersDTO>>(jsondata);
                if (obj != null)
                {
                    users = obj;
                }
            }
            return View(users);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO reg)
        {
            if (ModelState.IsValid)
            {
                string url = "https://localhost:7078/api/Auth/Register";
                string json = JsonConvert.SerializeObject(reg);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                string url = "https://localhost:7078/api/Auth/Login";
                string json = JsonConvert.SerializeObject(login);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(login);
        }

    }
}
