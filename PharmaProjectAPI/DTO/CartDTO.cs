namespace PharmaProjectAPI.DTO
{
    public class CartDTO
    {
        public int UserId { get; set; }
        public int MedicineId { get; set; }

        public int Quantity { get; set; }   
        public DateTime AddedAt { get; set; }

        //public string MedicineName { get; set; }
       

        public int Quantity { get; set; }

    }
}
