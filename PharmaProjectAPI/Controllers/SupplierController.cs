using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        ISupplierRepo repo;
        public SupplierController(ISupplierRepo repo) 
        { 
            this.repo = repo;
        }

        [HttpPost]
        [Route("AddSup")]
        public IActionResult AddSupplier(SupplierDTO dto)
        {
            repo.AddSupplier(dto);
            return Ok("Added Successfully");
        }

        [HttpGet]
        [Route("FetchSup")]
        public IActionResult FetchSupplier()
        {
            var data=repo.GetAll();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetSup/{id}")]
        public IActionResult GetSuppier(int id)
        {
            var data=repo.GetSupplierById(id);
            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateSup")]
        public IActionResult UpdateSupplier(SupplierDTO3 dto)
        {
            repo.UpdateSupplier(dto);
            return Ok("Updated Successfully");
        }

        [HttpDelete]
        [Route("DeleteSup/{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            repo.DeleteSupplier(id);
            return Ok("Deleted Successfully");
        }
    }
}
