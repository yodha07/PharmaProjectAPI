using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "Cashier";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
