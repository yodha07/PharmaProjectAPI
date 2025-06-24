using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface IMedicine
    {
        void AddMedi(MedicineDTO m);
        void Delete(int id);
        Medicine GetMedicineId(int id);
        //void edit(Medicine m);
        void Edit(MedicineEdit edit);
        List<Medicine> fetch();
        List<MedicineStockDto> GetMedicineStock();
        List<MedicineCartDTO> GetMedicinesForCart();
    }
}
