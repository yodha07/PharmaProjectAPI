using AutoMapper;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class PurchaseService : IPurchaseRepo
    {
        ApplicationDbContext db;
        IMapper mapper;
        public PurchaseService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public List<PurchaseMedDTO> GetMedicines()
        {
            var data=db.Medicines.ToList();
            var medicine=mapper.Map<List<PurchaseMedDTO>>(data);
            return medicine;
        }

    }
}
