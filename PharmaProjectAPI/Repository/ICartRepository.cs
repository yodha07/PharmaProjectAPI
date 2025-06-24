using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface ICartRepository
    {
        void AddToCart(Cart cart);
        List<CartDTO2> GetCartItems(int id);
        void DeleteCart(int id);

        List<MedicineCardDTO> GetAllAvailableMedicineCards();
    }
}
