using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class PurchaseItem
    {
        [Key]
        public int PurchaseItemId { get; set; }
        public int PurchaseId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        [ForeignKey("PurchaseId")]
        public Purchase Purchase { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }
        public List<SaleItem> SaleItems { get; set; }
    }
}
