﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmaProject.Models;
using static System.Net.WebRequestMethods;
using System.Text;
using System.Text.Unicode;
using PharmaProject.Data;
using PharmaProject.Filters;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace PharmaProject.Controllers
{
    [GlobalException]
    public class AuthController : Controller
    {
        HttpClient client;
        private readonly ApplicationDbContext db;

        public AuthController(ApplicationDbContext db)
        {
            this.db = db;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true;
            client = new HttpClient(clientHandler);
        }

        [Authorize(Roles = "Admin,Pharmacist, Cashier")]
        public IActionResult Index()
        {
            //throw new Exception("An error occurred while loading the user list.");
            List<UsersDTO> users = new List<UsersDTO>();
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

        [HttpGet]
        public IActionResult EditProfile(int id)
        {
            UpdateProfileVM vm = new UpdateProfileVM();
            string url = $"https://localhost:7078/api/Auth/GetUser/{id}";
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var obj = JsonConvert.DeserializeObject<UpdateProfileVM> (json);
                if(obj != null)
                {
                    vm = obj;
                }
            }
            return View(vm);
        }

        [HttpPost]
        public IActionResult EditProfile(UpdateProfileVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            string url = "https://localhost:7078/api/Auth/UpdateProfile";

            var jsonData = JsonConvert.SerializeObject(vm);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                // Optionally: TempData["Success"] = "Profile updated successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                // Optionally: Read error message from API
                var errorMsg = response.Content.ReadAsStringAsync().Result;
                ModelState.AddModelError("", $"Update failed: {errorMsg}");
                return View(vm);
            }
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
                    var role = db.Users.Where(u => u.Username == login.Username)
                        .Select(u => u.Role)
                        .FirstOrDefault();

                    var claims = new List<Claim>
{
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Role, role! ) // 👈 role from API response
};

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index");
                }
            }
            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string url = $"https://localhost:7078/api/Auth/DeleteUser/{id}";
            HttpResponseMessage response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public JsonResult CheckUsername(string username)
        {
            bool exists = db.Users.Any(u => u.Username == username);
            return Json(!exists);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }


        [AcceptVerbs("Post", "Get")]
        public IActionResult CheckUsernameVal(string username)
        {
            var usern = db.Users.Where(x => x.Username == username).FirstOrDefault();
            if (usern != null)
            {
                return Json($"Username {username} is already taken.");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
