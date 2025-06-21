using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public decimal PricePerUnit { get; set; }
        public string BatchNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }

        public List<PurchaseCart> PurchaseCarts { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }
        public List<SaleItem> SaleItems { get; set; }
        public List<Cart> Carts { get; set; }
    }
}
