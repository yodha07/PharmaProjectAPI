using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class medicineController : ControllerBase
    {
        private readonly IMedicine med;
        public medicineController(IMedicine med)
        {
            this.med = med;
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult AddMedicine(MedicineDTO m)
        {
            med.AddMedi(m);
            return Ok("Added Successfull!!!");
        }
        [HttpGet]
        [Route("fetch")]
        public IActionResult Fetch()
        {
            var t=med.fetch();
            return Ok(t);
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            med.Delete(id);
            return Ok("Deleted Successfull!!!");
        }
        [HttpGet]
        [Route("EditById/{id}")]
        public IActionResult GetMedicineId(int id)
        {
           var d= med.GetMedicineId(id);
            return Ok(d);
        }
        [HttpPut]
        [Route("Edit")]
        public IActionResult EditMedicine(int id, MedicineEdit edit)
        {
            med.Edit(id,edit);
            return Ok();
        }
    }
}
