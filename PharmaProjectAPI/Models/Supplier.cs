using System.ComponentModel.DataAnnotations;

namespace PharmaProjectAPI.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public List<PurchaseCart> PurchaseCarts { get; set; }
        public List<Purchase> Purchases { get; set; }
    }
}
