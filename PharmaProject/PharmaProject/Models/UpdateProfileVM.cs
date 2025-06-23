using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PharmaProject.Models
{
    public class UpdateProfileVM
    {
        public int UserID { get; set; }

        [Remote(action: "CheckUsernameVal", controller: "Auth")]
        [Required(ErrorMessage = "Username required")]
        public string UserName { get; set; } 

        [Required]
        public string Email { get; set; }
        public string Role { get; set; } = null!;

    }
}
