using PharmaProjectAPI.DTO;

namespace PharmaProjectAPI.Repository
{
    public interface ISupplierRepo
    {
        void AddSupplier(SupplierDTO supplier);
        List<SupplierDTO2> GetAll();
        SupplierDTO3 GetSupplierById(int id);
        void UpdateSupplier(SupplierDTO3 supplier);

        void DeleteSupplier(int id);
    }
}
