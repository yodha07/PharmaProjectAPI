using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public AuthController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        [Route ("Register")]
        public async Task<IActionResult> Register(Register reg)
        {
            if(db.Users.Any(u => u.Email.Equals(reg.Email) || u.Username.Equals(reg.Username)))
            {
                return BadRequest("User with this email or username already exists");
            }

            var user = new User{
                Username = reg.Username,
                Email = reg.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(reg.PasswordHash),
                Role = reg.Role
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return Ok("Registration successful");
        }
    }
}
