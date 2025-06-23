using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaProjectAPI.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int SaleId {  get; set; }
        public string PaymentId { get; set; }
        public decimal Amount {  get; set; }
        public string PaymentStatus {  get; set; }
        public DateTime PaymentDate { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }
    }
}
