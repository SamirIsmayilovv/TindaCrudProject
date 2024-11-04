using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Project.Data.DTOs.Account;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        public IdentityService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Responsee> Login(LoginDto dto)
        {
            IdentityUser user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return new Responsee() { Message = "Registered Succesfully!", StatusCode = 200 };
            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                return new Responsee() { Message = "User Not Found!", StatusCode = 404 };

            //jwt tokeninden isitifade
            string keyStr = "9428474e-ca30-4259-9c32-598010640788";
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken tokenJwt = new JwtSecurityToken(expires: DateTime.Now.AddDays(3), issuer: "http://localhost7199/",
                audience: "http://localhost7199/", claims: claims, signingCredentials: credentials);

            string tokenHaner = new JwtSecurityTokenHandler().WriteToken(tokenJwt);

            return new Responsee() { StatusCode=200,Data=tokenHaner};
        }

        public async Task<Responsee> Register(RegisterDto dto)
        {
            IdentityUser user = new()
            {
                UserName = dto.UserName,
                Email = dto.Email,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new Responsee() { StatusCode = 400, Message = "BadRequest" };
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            return new Responsee()
            {
                Message = "Registered Succesfully!",
                StatusCode = 200,
            };
        }
    }
}
