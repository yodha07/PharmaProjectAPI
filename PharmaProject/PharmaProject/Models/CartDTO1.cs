namespace PharmaProject.Models
{
    public class CartDTO1
    {
        public int UserId { get; set; }
        public int MedicineId { get; set; }

        public int Quantity { get; set; }
        public decimal ppu { get; set; }

        public DateTime AddedAt { get; set; }
    }
}
