using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaProjectAPI.Models
{
    public class Cart
    {
        public int CartId {  get; set; }
        public int UserId {  get; set; }


        public int MedicineId { get; set; } 
        public int Quantity {  get; set; }
        public DateTime AddedAt { get; set; }
        //public decimal ppu{get; set;}

        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }


        public string MedicineId {  get; set; }
        public int Quantity {  get; set; }
        public DateTime AddedAt { get; set; }

        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }

    }
}
