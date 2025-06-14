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
    }
}
