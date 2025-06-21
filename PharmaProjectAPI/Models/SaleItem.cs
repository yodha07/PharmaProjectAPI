using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class SaleItem
    {
        [Key]
        public int ItemId { get; set; }
        public int SaleId { get; set; }
        public int MedicineId { get; set; }
        public int PurchaseItemId { get; set; }
        public int? CustomerId {  get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }

        [ForeignKey("PurchaseItemId")]
        public PurchaseItem PurchaseItem { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer{ get; set; }
    }
}
