using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.DTO
{
    public class UsersDTO
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } 
        public DateTime CreatedDate { get; set; }
    }
}
