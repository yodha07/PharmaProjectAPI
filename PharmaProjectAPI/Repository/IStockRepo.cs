using PharmaProjectAPI.DTO;

namespace PharmaProjectAPI.Repository
{
    public interface IStockRepo
    {
        bool AdjustStock(StockUpdateDTO dto, bool add);
    }
}
