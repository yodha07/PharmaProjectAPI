namespace PharmaProject.Models
{
    public class BillItemDto
    {
        public int SrNo { get; set; }
        public string Product { get; set; }
        public string Batch { get; set; }
        public int Strip { get; set; }
        public decimal Mrp { get; set; }
        public decimal Disc { get; set; }
        public decimal Amt { get; set; }
    }
}
