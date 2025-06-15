using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }
    }
}
