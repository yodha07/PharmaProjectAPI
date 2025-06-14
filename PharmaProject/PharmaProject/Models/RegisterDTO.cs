using System.ComponentModel.DataAnnotations;

namespace PharmaProject.Models
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = "Cashier";
    }
}
