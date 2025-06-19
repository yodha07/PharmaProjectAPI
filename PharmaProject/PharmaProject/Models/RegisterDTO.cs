using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PharmaProject.Models
{
    public class RegisterDTO
    {
        [Remote(action: "CheckUsernameVal", controller: "Auth")]
        [Required(ErrorMessage = "Username required")]
        public string Username { get; set; } = null!;

        [EmailAddress]
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = "Cashier";
    }
}
