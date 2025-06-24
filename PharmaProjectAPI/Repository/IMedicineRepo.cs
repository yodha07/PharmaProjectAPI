using PharmaProjectAPI.DTO;

namespace PharmaProjectAPI.Repository
{
    public interface IMedicineRepo
    {
        Task<List<MedicineStockDto>> GetAvailableMedicinesAsync();
        List<MedicineCartDTO> GetMedicinesForCart();
    }
}
