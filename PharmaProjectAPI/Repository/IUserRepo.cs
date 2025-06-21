using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface IUserRepo 
    {
        Task Register(User user);
        bool UserExists(Register reg);
        Task<string> Login(Login login);
        List<UsersDTO> GetUser();
        Task <bool> SendEmailAsync(string toEmail, string subject, string body);
        void DeleteUserById(int id);
        Task<string> GetUserRole(string username);
    }
}
