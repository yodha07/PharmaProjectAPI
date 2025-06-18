namespace PharmaProjectAPI.DTO
{
    public class ExpiredMedicineDTO
    {
        public string Name { get; set; } = string.Empty;
        public string BatchNo { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
    }
}
