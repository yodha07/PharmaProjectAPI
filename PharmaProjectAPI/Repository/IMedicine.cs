using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface IMedicine
    {
        void AddMedi(MedicineDTO m);
        void Delete(int id);
        MedicineEdit GetMedicineId(int id);
        void Edit(int id,MedicineEdit edit);
       List<Medicine> fetch();
    }
}
