using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        IPurchaseRepo repo;
        public PurchaseController(IPurchaseRepo repo) 
        { 
            this.repo = repo;
        }

        [HttpGet]
        [Route("GetMed")]
        public IActionResult GetMedicines()
        {
            var data=repo.GetMedicines();
            return Ok(data);
        }

        [HttpPost]
        [Route("AddCart")]
        public IActionResult AddCart(PurchaseCartDTO dto)
        {
            repo.AddCart(dto);
            return Ok("Added Successfully");
        }

        [HttpGet]
        [Route("FetchCart")]
        public IActionResult FetchCart()
        {
            var data = repo.GetAll();
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteSingleCart/{id}")]
        public IActionResult DeleteSingleCart(int id)
        {
            repo.DeleteSingleCart(id);
            return Ok("Deleted Successfully");
        }

        [HttpPost]
        [Route("DeleteCart")]
        public IActionResult DeleteCart([FromBody] List<int> ids)
        {
            repo.DeleteCart(ids);
            return Ok("Cart Deleted");
        }

        //[HttpDelete]
        //[Route("DeleteCart")]
        //public IActionResult DeleteCart(List<int> ids)
        //{
        //    repo.DeleteCart(ids);
        //    return Ok("Cart Deleted");
        //}

        [HttpPost]
        [Route("AddPurchase")]
        public IActionResult AddPurchase(PurchaseDTO dto)
        {
            repo.AddPurchase(dto);
            return Ok("Added Successfully");
        }

        [HttpGet]
        [Route("FetchPurchase")]
        public IActionResult FetchPurchase()
        {
            var data = repo.GetPurchase();
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeletePurchase/{id}")]
        public IActionResult DeletePurchase(int id)
        {
            int r = repo.DeletePurchase(id);
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
        [Route("AddPurchaseItem")]
        public IActionResult AddPurchaseItem(PurchaseItemDTO dto)
        {
            repo.AddPurchaseItem(dto);
            return Ok("Added Successfully");
        }

        [HttpGet]
        [Route("FetchPurchaseItem/{id}")]
        public IActionResult FetchPurchaseItem(int id)
        {
            var data = repo.GetPurchaseItem(id);
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeletePurchaseItem/{id}")]
        public IActionResult DeletePurchaseItem(int id)
        {
            int r = repo.DeletePurchaseItem(id);
            if (r > 0)
            {
                return Ok("Deleted Successfully");
            }
            else
            {
                return NotFound("Cannot delete this record");
            }
        }

    }
}
