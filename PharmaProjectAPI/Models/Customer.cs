using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string EmailId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public List<Sale> Sales { get; set; }
        public List<SaleItem> SaleItems { get; set; }
    }
}
