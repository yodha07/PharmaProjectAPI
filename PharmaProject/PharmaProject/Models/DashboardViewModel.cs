namespace PharmaProject.Models
{
    public class DashboardViewModel
    {
        public int StockAlertCount { get; set; }
        public int ExpAlertCount { get; set; }
        public int PriorExpAlertCount { get; set; }
        public List<PurchaseItemDtoSF> StockAlertTable { get; set; }
        public List<PurchaseItemDtoSF> ExpAlertTable { get; set; }
        public List<PurchaseItemDtoSF> PriorExpAlertTable { get; set; }
        public List<TodaySaleDto> TodaySales {  get; set; }
        public List<Top5Dto> Top5 {  get; set; }


    }
}
