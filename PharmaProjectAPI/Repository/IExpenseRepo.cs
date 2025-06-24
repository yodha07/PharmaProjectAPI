using PharmaProjectAPI.Models;

namespace PharmaProjectAPI.Repository
{
    public interface IExpenseRepo
    {
        List<Expense> GetAllExpenses();
        Expense AddExpense(Expense expense);
        decimal GetTotalExpenses();
    }
}
