using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class PurchaseCart
    {
        [Key]
        public int CartId { get; set; }
        public int? SupplierId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal Ppu { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }
    }
}
