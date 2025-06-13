using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class UserService : IUserRepo
    {
        private readonly ApplicationDbContext db;

        private readonly IMapper mapper;

        public UserService(ApplicationDbContext db, IMapper mapper)
        {
            this.mapper = mapper;
            this.db = db;
        }

        public async Task Register(User user)
        {
            db.Users.Add(user);
            db.SaveChangesAsync();
        }

        public bool UserExists(Register reg)
        {
            return db.Users.Any(u => u.Username == reg.Username || u.Email == reg.Email);
        }

        public async Task<string> Login(Login login)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user == null)
                return "User not found";

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash);

            if (!isPasswordCorrect)
                return "Invalid password";

            return "Login successful";
        }
    }
}
