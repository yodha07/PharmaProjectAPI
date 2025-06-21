namespace PharmaProjectAPI.DTO
{
    public class PurchaseDTO
    {
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalAmount { get; set; }
        public string CreatedBy { get; set; }
    }
}
