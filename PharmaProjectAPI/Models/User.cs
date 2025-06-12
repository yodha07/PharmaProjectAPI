using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; } 
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; } // Admin, Pharmacist, Cashier
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
