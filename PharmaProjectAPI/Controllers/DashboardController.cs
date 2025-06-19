using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMapper mapper;

        private readonly IDashboard repo;

        public DashboardController(IMapper mapper, IDashboard repo)
        {
            this.mapper = mapper;
            this.repo = repo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Cashier")]
        [Route("GetTotalMedicines")]
        public async Task<IActionResult> GetTotalMedicines()
        {
            var data = await repo.GetTotalMedicines();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetTotalSales")]
        public async Task<IActionResult> GetTotalSales()
        {
            var data = await repo.GetTotalSales();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetExpiringSoon")]
        public async Task<IActionResult> GetExpiringSoon()
        {
            var data = await repo.GetExpiringSoon();
            if (data != 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound("No medicines expiring soon");
            }
        }

        [HttpGet]
        [Route("GetLowStock")]
        public async Task<IActionResult> GetLowStock()
        {
            var data = await repo.GetLowStock();
            if (data != 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound("No medicines in low stock");
            }
        }

        [HttpGet]
        [Route("GetMonthlySales")]
        public async Task<IActionResult> GetMonthlySales()
        {
            var data = await repo.GetMonthlySales();
            if (data != null && data.Count > 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound("No monthly sales data available");
            }
        }

        [HttpGet]
        [Route("GetTopMedicines")]
        public async Task<IActionResult> GetTopMedicines(int count = 5)
        {
            var data = await repo.GetTopMedicines(count);
            if (data != null && data.Count > 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound("No top medicines data available");
            }
        }

        [HttpGet]
        [Route("GetRecentSales")]
        public async Task<IActionResult> GetRecentSales(int count = 10)
        {
            var data = await repo.GetRecentSales(count);
            if (data != null && data.Count > 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound("No recent sales data available");
            }
        }

        [HttpGet]
        [Route("GetExpiredMedicines")]
        public async Task<IActionResult> GetExpiredMedicines(int count = 10)
        {
            var data = await repo.GetExpiredMedicines(count);
            if (data != null && data.Count > 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound("No expired medicines found");
            }
        }

        [HttpGet]
        [Route("GetStockSummaries")]
        public async Task<IActionResult> GetStockSummaries(int count = 10)
        {
            var data = await repo.GetStockSummaries(count);
            if (data != null && data.Count > 0)
            {
                return Ok(data);
            }
            else
            {
                return NotFound("No stock summaries available");
            }
        }
    }

}
