using Project.Data.DTOs.Tipe;
using Project.Service.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Interfaces
{
    public interface ITipeService
    {
        public Task<Responsee> CreateAsync(TipePostDto dto);
        public Task<Responsee> GetAllAsync();
        public Task<Responsee> GetAsync(int id);
        public Task<Responsee> UpdateAsync(int id, TipePutDto dto);
        public Task<Responsee> SoftDeleteAsync(int id);
        public Task<Responsee> HardDeleteAsync(int id);
        public Task<Responsee> GetProductsByType(int id);
    }
}
