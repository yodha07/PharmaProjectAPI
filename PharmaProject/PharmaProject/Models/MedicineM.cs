namespace PharmaProject.Models
{
    public class MedicineM
    {
            public int medicineId { get; set; }
            public string name { get; set; }
            public string category { get; set; }
            public string manufacturer { get; set; }
            public float pricePerUnit { get; set; }
            public string? batchNo { get; set; }
            public DateTime expiryDate { get; set; }
            public DateTime createdAt { get; set; }
            public string createdBy { get; set; }
            public DateTime modifiedAt { get; set; }
            public string modifiedBy { get; set; }
            public object purchaseItems { get; set; }
            public object saleItems { get; set; }
        

    }
}
