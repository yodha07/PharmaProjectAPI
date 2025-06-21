using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface ITransactionRepo
    {
        void AddTransaction(Transaction transaction);
        List<Transaction> GetTransactions();
    }
}
