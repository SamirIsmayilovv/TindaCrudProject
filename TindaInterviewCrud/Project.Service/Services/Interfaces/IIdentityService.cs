using Project.Data.DTOs.Account;
using Project.Service.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Interfaces
{
    public interface IIdentityService
    {
        public Task<Responsee> Register(RegisterDto dto);
        public Task<Responsee> Login(LoginDto dto);
    }
}
