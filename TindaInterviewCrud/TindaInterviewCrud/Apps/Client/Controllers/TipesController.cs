using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DTOs.Tipe;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;

namespace TindaInterviewCrud.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipesController : ControllerBase
    {
        private readonly ITipeService _service;
        public TipesController(ITipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Responsee response = await _service.GetAllAsync();
            if (response.StatusCode == 204)
                return StatusCode(204, "There is no Type");
            return StatusCode(response.StatusCode, response.Data);
        }
     

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Responsee response = await _service.GetAsync(id);
            if (response.StatusCode == 404)
                return StatusCode(404, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

       

        [HttpGet("{typeId}/products")]
        public async Task<IActionResult> GetProductsByTypeId(int typeId)
        {
            Responsee response = await _service.GetProductsByType(typeId);
            if(response.StatusCode==204 || response.StatusCode==404)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

       
    }
}
