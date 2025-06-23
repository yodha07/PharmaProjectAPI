using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.DTO
{
    public class UpdateProfileDTO
    {
        public int UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
        public string Role { get; set; } = null!;

    }
}
