using PharmaProjectAPI.Data;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class TransactionService: ITransactionRepo
    {
        private readonly ApplicationDbContext db;
        public TransactionService(ApplicationDbContext db) 
        {
            this.db = db;
        }

        public void AddTransaction(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }
        public List<Transaction> GetTransactions()
        {
            return db.Transactions.ToList();
        }
    }
}
