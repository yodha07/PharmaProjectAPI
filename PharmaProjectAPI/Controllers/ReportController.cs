using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IReportsRepository repo;

        public ReportController(ApplicationDbContext db, IReportsRepository repo)
        {
            this.db = db;
            this.repo = repo;
        }

        [HttpGet]
        [Route("StockAlert")]
        public IActionResult stockAlert()
        {
            int count = repo.stockAlert();
            return Ok(count);
        }

        [HttpGet]
        [Route("ExpAlert")]
        public IActionResult ExpAlert()
        {
            int expcount = repo.ExpAlert();
            return Ok(expcount);
        }

        [HttpGet]
        [Route("PriorExpAlert")]
        public IActionResult PriorExpAlert()
        {
            int priorexpalert = repo.PriorExpAlert();
            return Ok(priorexpalert);

        }

        [HttpGet]
        [Route("StockAlertTable")]
        public IActionResult stockAlertTable()
        {

            var stockAlertTable = repo.stockAlertTable();
            return Ok(stockAlertTable);
        }

        [HttpGet]
        [Route("ExpAlertTable")]
        public IActionResult ExpAlertTable()
        {
            var expAlertTable = repo.ExpAlertTable();
            return Ok(expAlertTable);
        }

        [HttpGet]
        [Route("TodaySale")]
        public IActionResult TodaySalesTable() 
        {
            var total_sale = repo.TodaySalesTable();
            return Ok(total_sale);
        }

        [HttpGet]
        [Route("Top5")]
        public IActionResult Top5()
        {
            var top_sale = repo.Top5();
            return Ok(top_sale);
        }

        [HttpGet]
        [Route("PriorExpAlertTable")]
        public IActionResult PriorExpAlertTable()
        {
            var priorexpalert = repo.PriorExpAlertTable();
            return Ok(priorexpalert);
        }
    }
}
