using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly IUserRepo repo;

        public AuthController(IMapper mapper, IUserRepo repo)
        {
            this.mapper = mapper;
            this.repo = repo;
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
            user.Role = "Cashier";
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
    }
}
