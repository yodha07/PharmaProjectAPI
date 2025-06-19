using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PharmaProject.Models
{
    public class UsersDTO
    {
        [Key]
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
