using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.DTO
{
    public class SupplierDTO
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string CreatedBy { get; set; }
    }
}
