using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DTOs.Product;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;

namespace TindaInterviewCrud.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([FromBody]ProductPostDto dto)
        {
            Responsee response = await _service.CreateAsync(dto);
            if (response.StatusCode == 400 || response.StatusCode==404)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Responsee response=await _service.DeleteAsync(id);
            if(response.StatusCode==404)
                return StatusCode(404,response.Message);
            return StatusCode(200, response.Message);
        }
        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(ProductPutDto dto)
        {
            Responsee response= await _service.UpdateAsync(dto);
            return StatusCode(response.StatusCode, response.Message);
        }

    }
}
