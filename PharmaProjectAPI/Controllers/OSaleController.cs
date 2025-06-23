using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OSaleController : ControllerBase
    {
        IOSaleRepo repo;
        public OSaleController(IOSaleRepo repo)
        {
            this.repo = repo;
        }
        [HttpPost]
        [Route("AddSale")]
        public IActionResult AddSale(OSaleDTO1 dto)
        {
            repo.AddSale(dto);
            return Ok("Added Successfully");
        }

        [HttpGet]
        [Route("FetchSale")]
        public IActionResult FetchSale()
        {
            var data = repo.GetSale();
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteSale/{id}")]
        public IActionResult DeleteSale(int id)
        {
            int r = repo.DeleteSale(id);
            if (r > 0)
            {
                return Ok("Deleted Successfully");
            }
            else
            {
                return NotFound("Cannot delete this record");
            }
        }

        [HttpPost]
        [Route("AddSaleItem")]
        public IActionResult AddSaleItem(OSaleItemDTO1 dto)
        {
            repo.AddSaleItem(dto);
            return Ok("Added Successfully");
        }

        [HttpGet]
        [Route("FetchSaleItem/{id}")]
        public IActionResult FetchSaleItem(int id)
        {
            var data = repo.GetSaleItem(id);
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteSaleItem/{id}")]
        public IActionResult DeleteSaleItem(int id)
        {
            repo.DeleteSaleItem(id);
            return Ok("Deleted Successfully");
        }
    }
}
