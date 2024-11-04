using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DTOs.Product;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;

namespace TindaInterviewCrud.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

       

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Responsee response = await _service.GetAllAsync();
            if (response.StatusCode == 404)
                return StatusCode(404, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpGet("product/{id}")]  
        public async Task<IActionResult> Get(int id)
        {
            Responsee response = await _service.GetAsync(id);
            if (response.StatusCode == 404)
                return StatusCode(404, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpGet("{productId}/categories")]  
        public async Task<IActionResult> GetProductCategories(int productId)
        {
            Responsee response = await _service.GetCategoriesByProductId(productId);
            if (response.StatusCode == 404)
                return StatusCode(404, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpGet("{productId}/types")]
        public async Task<IActionResult> GetProductTypes(int productId)
        {
            Responsee response = await _service.GetTypesByProductId(productId);
            if (response.StatusCode == 404)
                return StatusCode(404, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

    }
}
