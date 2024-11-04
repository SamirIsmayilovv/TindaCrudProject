using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.Data.DTOs.Account;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TindaInterviewCrud.Apps.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdentityService _service;
        public AccountsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IIdentityService service)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _service = service; 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto dto)
        {
            Responsee response=await _service.Register(dto);
            return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
            return Ok("Created");
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery]LoginDto dto)
        {
            Responsee response=await _service.Login(dto);
            if (response.StatusCode == 404 || response.StatusCode == 200)
                return StatusCode(response.StatusCode, response.Message);
            return StatusCode(response.StatusCode, response.Data);
        }

    }
}
