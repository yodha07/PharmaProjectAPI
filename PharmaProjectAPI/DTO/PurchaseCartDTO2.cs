namespace PharmaProjectAPI.DTO
{
    public class PurchaseCartDTO2
    {
        public int CartId { get; set; }
        public int SupplierId { get; set; }
        public string Sname { get; set; }
        public int MedicineId { get; set; }
        public string Mname { get; set; }
        public int Quantity { get; set; }
        public decimal Ppu { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}
