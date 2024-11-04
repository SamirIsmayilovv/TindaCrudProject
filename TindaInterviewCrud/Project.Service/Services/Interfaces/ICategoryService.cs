using Azure;
using Project.Data.DTOs.Category;
using Project.Service.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<Responsee> CreateAsync(CategoryPostDto dto);
        public Task<Responsee> GetAllAsync();
        public Task<Responsee> GetAsync(int id);
        public Task<Responsee> UpdateAsync(int id, CategoryPutDto dto);
        public Task<Responsee> SoftDeleteAsync(int id);
        public Task<Responsee> HardDeleteAsync(int id);
        public Task<Responsee> GetProductsByCategory(int id);
    }
}
