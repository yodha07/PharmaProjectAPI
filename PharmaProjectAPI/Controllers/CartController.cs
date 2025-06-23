using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;

namespace PharmaProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository repo;
        private readonly IMapper mapper;
        public CartController(ICartRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddToCart(CartDTO dto)
        {
            var data = mapper.Map<Cart>(dto);
            data.AddedAt = DateTime.Now;
            repo.AddToCart(data);
            return Ok("Added Successfully");
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult DeleteCart(int id)
        {
            repo.DeleteCart(id);
            return Ok("Deleted Cart Item");
        }

        [HttpGet]
        [Route("fetch/{id}")]
        public IActionResult GetCart(int id)
        {
            var data = repo.GetCartItems(id);
            return Ok(data);
        }
    }
}
