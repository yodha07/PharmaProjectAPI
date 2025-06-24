using Newtonsoft.Json;

namespace PharmaProject.Models
{
    public class MedicineCartDTO
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        [JsonProperty("price")] // 🔥 THIS maps the JSON field to your C# property
        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; } = 0;
    }
}
