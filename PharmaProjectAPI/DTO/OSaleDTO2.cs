using PharmaProjectAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaProjectAPI.DTO
{
    public class OSaleDTO2
    {
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
