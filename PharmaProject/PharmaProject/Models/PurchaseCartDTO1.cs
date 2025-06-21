namespace PharmaProject.Models
{
    public class PurchaseCartDTO1
    {
        public int SupplierId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public decimal Ppu { get; set; }
        public string? CreatedBy { get; set; }
    }
}
