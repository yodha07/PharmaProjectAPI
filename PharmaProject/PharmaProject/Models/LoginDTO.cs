using Microsoft.Build.Framework;

namespace PharmaProject.Models
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;
    }
}
