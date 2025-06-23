using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepo _stockRepo;

        public StockController(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpPost("UpdateStock")]
        public IActionResult UpdateStock([FromQuery] bool add, [FromBody] StockUpdateDTO update)
        {
            var success = _stockRepo.AdjustStock(update, add);
            if (!success)
            { 
                return NotFound(); 
            }
            

            return Ok();
        }
    }

}
