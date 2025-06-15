using PharmaProjectAPI.DTO;

namespace PharmaProjectAPI.Repository
{
    public interface ISupplierRepo
    {
        void AddSupplier(SupplierDTO supplier);
        List<SupplierDTO2> GetAll();
        SupplierDTO GetSupplierById(int id);
        void UpdateSupplier(SupplierDTO supplier);

        void DeleteSupplier(int id);
    }
}
