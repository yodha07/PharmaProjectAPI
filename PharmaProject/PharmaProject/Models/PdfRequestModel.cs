namespace PharmaProject.Models
{
    public class PdfRequestModel
    {
        public List<PurchaseCartDTO2> CartItems { get; set; }
        public decimal Total { get; set; }
        public string InvoiceNo { get; set; }
    }

}
