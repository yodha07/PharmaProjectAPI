using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface IExpenseRepo
    {
        List<Expense> GetAllExpenses();
        void AddExpense(Expense expense);
        decimal GetTotalExpenses();
    }
}
