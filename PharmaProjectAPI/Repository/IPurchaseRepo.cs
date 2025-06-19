using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface IPurchaseRepo
    {
        List<PurchaseMedDTO> GetMedicines();
    }
}
