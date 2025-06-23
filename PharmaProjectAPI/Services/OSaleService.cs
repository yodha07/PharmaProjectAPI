using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class OSaleService : IOSaleRepo
    {
        ApplicationDbContext db;
        IMapper mapper;
        public OSaleService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public void AddSale(OSaleDTO1 sale) 
        {
            var data = mapper.Map<Sale>(sale);
            data.CreatedAt = DateTime.Now;
            db.Sales.Add(data);
            db.SaveChanges();
        }
        public List<OSaleDTO2> GetSale()
        {
            var data = db.Sales.Include(x => x.Customer).ToList();
            var sale = mapper.Map<List<OSaleDTO2>>(data);
            return sale;
        }
        public int DeleteSale(int id)
        {
            var sale = db.Sales.Include(x => x.SaleItems).FirstOrDefault(x => x.SaleId == id);
            if (sale.SaleItems.Count > 0)
            {
                return 0;
            }
            else
            {
                db.Sales.Remove(sale);
                db.SaveChanges();
                return 1;
            }
        }
        public void AddSaleItem(OSaleItemDTO1 saleitem)
        {
            var id = db.Sales.OrderByDescending(x => x.SaleId).Select(x => x.SaleId).FirstOrDefault();
            var cid = db.Sales.Where(x=>x.SaleId==id).Select(x => x.CustomerId).FirstOrDefault();
            var data = mapper.Map<SaleItem>(saleitem);
            data.SaleId = int.Parse((id).ToString());
            data.CreatedAt = DateTime.Now;
            data.CustomerId= int.Parse((cid).ToString());
            db.SaleItems.Add(data);
            db.SaveChanges();
        }
        public List<OSaleItemDTO2> GetSaleItem(int id)
        {
            var data = db.SaleItems.Include(x => x.Medicine).Include(x => x.Sale).Where(x => x.SaleId == id).ToList();
            var sale = mapper.Map<List<OSaleItemDTO2>>(data);
            return sale;
        }
        public void DeleteSaleItem(int id)
        {
            var saleitem = db.SaleItems.Find(id);
            db.SaleItems.Remove(saleitem);
            db.SaveChanges();
        }
    }
}
