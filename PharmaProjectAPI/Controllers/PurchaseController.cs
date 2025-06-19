using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
