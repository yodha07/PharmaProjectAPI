namespace PharmaProjectAPI.DTO
{
    public class SupplierDTO2
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}
