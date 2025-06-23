using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
            user.Role = "Admin";
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

            

            var user = await repo.GetUserByUsername(login.Username);
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            var role = await repo.GetUserRole(login.Username);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, login.Username),
                new Claim(ClaimTypes.Role, role) 
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Ok("Login successful");
        }

        //[HttpPut]
        //[Route ("UpdateProfile")]
        //public async Task<IActionResult> UpdateProfile(UpdateProfileDTO upd)
        //{
        //    var username = User.Identity?.Name;
        //    if(username == null)
        //    {
        //        return Unauthorized("User not found");
        //    }

        //    var getUser = await repo.GetUserByUsername(username);
        //    if(getUser == null)
        //    {
        //        return Unauthorized("User not found");
        //    }

        //    var exists = repo.UserExistsWithEmail(upd.UserName, upd.Email, getUser.UserId);
        //    if (await exists)
        //    {
        //        return BadRequest("Email or username already in use");
        //    }

        //    getUser.Username = upd.UserName;
        //    getUser.Email = upd.Email;
        //    getUser.Role = upd.Role;

        //    await repo.UpdateUser(getUser);

        //    return Ok("updated");

        //}

        [HttpGet]
        [Route("GetUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await repo.GetUserByID(id);
            if (user == null) return NotFound();

            var dto = mapper.Map<UpdateProfileDTO>(user);
            return Ok(dto);
        }

        [HttpPut]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDTO upd)
        {
            var user = await repo.GetUserByID(upd.UserID);
            if (user == null) return NotFound();

            var exists = await repo.UserExistsWithEmail(user.Email, user.Username, user.UserId);
            if (!exists) return NotFound();

            user.Email = upd.Email;
            user.Username = upd.UserName;
            user.Role = upd.Role;

            await repo.UpdateUser(user);
            return Ok("Updated");
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
