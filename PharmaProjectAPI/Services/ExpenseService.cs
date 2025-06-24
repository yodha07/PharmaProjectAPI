using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Services
{
    public class ExpenseService:IExpenseRepo
    {
        private readonly ApplicationDbContext db;

        public ExpenseService(ApplicationDbContext db)
        {
           this.db = db;
        }

        public List<Expense> GetAllExpenses()
        {
            return db.Expenses.ToList();
        }

        public void AddExpense(Expense expense)
        {
            db.Expenses.Add(expense);
            db.SaveChanges();
        }

        public decimal GetTotalExpenses()
        {
            return db.Expenses.Sum(e => e.Amount);
        }
    }
}

