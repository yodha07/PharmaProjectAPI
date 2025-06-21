namespace PharmaProject.Models
{
    public class PurchaseDTO2
    {
        public int PurchaseId { get; set; }
        public int SupplierId { get; set; }
        public string Sname { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
