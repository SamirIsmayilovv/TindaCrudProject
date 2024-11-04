using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DTOs.Category;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;

namespace TindaInterviewCrud.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

      

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "{Admin}")]
        public async Task<IActionResult> Create([FromBody]CategoryPostDto dto)
        {
            Responsee response = await _service.CreateAsync(dto);
            if (response.StatusCode == 400)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Responsee response = await _service.SoftDeleteAsync(id);
            return StatusCode(response.StatusCode, response.Message);
        }

        
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(CategoryPutDto dto)
        {
            Responsee response = await _service.UpdateAsync(dto.Id, dto);
            if (response.StatusCode == 404)
                return StatusCode(404, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }
        [HttpDelete("{Id}/harddelete")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> HardDelete(int id)
        {
            Responsee responsee = await _service.HardDeleteAsync(id);
            return StatusCode(responsee.StatusCode, responsee.Message);
        }

     


    }
}
