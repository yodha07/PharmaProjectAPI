using System.ComponentModel.DataAnnotations;

namespace PharmaProject.Models
{
    public class SupplierDTO3
    {
        public int SupplierId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Contact { get; set; }
        [Required]
        public string Address { get; set; }
        public string ModifiedBy { get; set; }
    }
}
