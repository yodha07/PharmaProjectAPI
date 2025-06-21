using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;
using System.Security.Claims;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly IUserRepo repo;

        private readonly ApplicationDbContext db;
        public AuthController(IMapper mapper, IUserRepo repo, ApplicationDbContext db)
        {
            this.mapper = mapper;
            this.repo = repo;
            this.db = db;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Register reg)
        {
            var res = repo.UserExists(reg);
            if (res)
            {
                return BadRequest("User already exists with this username or email");
            }

            string pass = BCrypt.Net.BCrypt.HashPassword(reg.PasswordHash);
            var user = mapper.Map<User>(reg);
            user.Role = "User";
            user.PasswordHash = pass;
            user.CreatedDate = DateTime.Now;

            await repo.Register(user);

            await repo.SendEmailAsync(reg.Email, "Registration Succesful", "Welcome to Pharma Suite\n" +
                "Hope you find all your pharmacetucal needs!\n" +
                "Thank You for your support!!");
            return Ok("Registration successful");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login login)
        {
            var res = await repo.Login(login);
            if (res != "Login successful")
            {
                return BadRequest(res);
            }

            var role = repo.GetUserRole(login.Username);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, await role) 
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok("Login successful");
        }

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser()
        {
            var userList = repo.GetUser();
            if (userList.Count == 0)
            {
                return NotFound("No user found with the provided username or email");
            }
            return Ok(userList);
        }


        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = repo.GetUser().FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound("User not found");
            }
            
            repo.DeleteUserById(id);
            return Ok("User deleted successfully");
        }
    }
}
