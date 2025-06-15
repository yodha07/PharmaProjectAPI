using System.ComponentModel.DataAnnotations;

namespace PharmaProject.Models
{
    public class SupplierDTO1
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Address { get; set; }
        public string CreatedBy { get; set; }
    }
}
