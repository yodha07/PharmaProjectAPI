using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;
using PharmaProjectAPI.Services;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseRepo repo;

        public ExpensesController(IExpenseRepo repo)
        {
            this.repo = repo;
        }

        [HttpPost("add")]
        public IActionResult AddExpense( ExpenseDTO dto)
        {
            var expense = new Expense
            {
                Category = dto.Category,
                Amount = dto.Amount,
                Date = dto.Date,
                CreatedAt = DateTime.Now,
                CreatedBy = "Admin"
            };

            repo.AddExpense(expense);
            return Ok("Expense added successfully");
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var data = repo.GetAllExpenses();
            return Ok(data);
        }

        [HttpGet("profit")]
        public IActionResult GetProfit( decimal totalSales, decimal totalCostOfGoods)
        {
            var totalExpenses = repo.GetTotalExpenses();
            var profit = totalSales - (totalCostOfGoods + totalExpenses);
            return Ok(profit);
        }
    }
}

