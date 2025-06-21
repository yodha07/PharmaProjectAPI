using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface IPurchaseRepo
    {
        List<PurchaseMedDTO> GetMedicines();
        void AddCart(PurchaseCartDTO cart);
        List<PurchaseCartDTO2> GetAll();
        void DeleteSingleCart(int id);
        void DeleteCart(List<int> ids);
        void AddPurchase(PurchaseDTO purchase);
        List<PurchaseDTO2> GetPurchase();
        int DeletePurchase(int id);
        void AddPurchaseItem(PurchaseItemDTO purchaseitem);
        List<PurchaseItemDTO2> GetPurchaseItem(int id);
        int DeletePurchaseItem(int id);
    }
}
