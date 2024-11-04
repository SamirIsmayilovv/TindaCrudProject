using Project.Data.DTOs.Product;
using Project.Service.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Interfaces
{
    public interface IProductService
    {
        public Task<Responsee> CreateAsync(ProductPostDto dto);
        public Task<Responsee> GetAllAsync();
        public Task<Responsee> GetAsync(int id);
        public Task<Responsee> UpdateAsync(ProductPutDto dto);
        public Task<Responsee> DeleteAsync(int id);
        public Task<Responsee> GetCategoriesByProductId(int id);
        public Task<Responsee> GetTypesByProductId(int id);
        
    }
}
