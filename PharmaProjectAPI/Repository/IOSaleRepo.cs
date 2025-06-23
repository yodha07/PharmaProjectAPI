using PharmaProjectAPI.DTO;

namespace PharmaProjectAPI.Repository
{
    public interface IOSaleRepo
    {
        void AddSale(OSaleDTO1 sale);
        List<OSaleDTO2> GetSale();
        int DeleteSale(int id);
        void AddSaleItem(OSaleItemDTO1 saleitem);
        List<OSaleItemDTO2> GetSaleItem(int id);
        void DeleteSaleItem(int id);
    }
}
