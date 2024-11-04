using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Data.DTOs.Tipe;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;

namespace TindaInterviewCrud.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [Authorize(Roles ="Admin")]
    [ApiController]
    public class TipesController : ControllerBase
    {
        private readonly ITipeService _service;
        public TipesController(ITipeService service)
        {
            _service = service;
        }

       
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TipePostDto dto)
        {
            Responsee response = await _service.CreateAsync(dto);
            if (response.StatusCode == 400)
                return StatusCode(400, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

     

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Responsee response = await _service.SoftDeleteAsync(id);
            return StatusCode(response.StatusCode, response.Message);
        }

       

        [HttpPut()]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(int id,TipePutDto dto)
        {
            Responsee response=await _service.UpdateAsync(id, dto);
            return StatusCode(response.StatusCode,response.Data);
        }
    }
}
