using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
        public List<SaleItem> SaleItems { get; set; }
    }
}
