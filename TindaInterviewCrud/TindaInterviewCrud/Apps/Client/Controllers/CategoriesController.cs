using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DTOs.Category;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;

namespace TindaInterviewCrud.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Responsee response = await _service.GetAllAsync();
            if (response.StatusCode == 204)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

      

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Responsee response = await _service.GetAsync(id);
            if (response.StatusCode == 404)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }
        
      

        [HttpGet("{categoryId}/products")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            Responsee response = await _service.GetProductsByCategory(categoryId);
            if (response.StatusCode == 404)
                return StatusCode(404, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

       


    }
}
