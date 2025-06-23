using PharmaProjectAPI.DTO;

namespace PharmaProjectAPI.Repository
{
    public interface IReportsRepository
    {
        int stockAlert();
        int ExpAlert();
        int PriorExpAlert();
        List<PurchaseItemDtoSF> stockAlertTable();
        List<PurchaseItemDtoSF> ExpAlertTable();
        List<TodaySaleDto> TodaySalesTable();
        List<Top5Dto> Top5();
        List<PurchaseItemDtoSF> PriorExpAlertTable();
    }
}
