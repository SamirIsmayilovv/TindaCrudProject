using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
        public Task CreateProductCategory(ProductCategory product);
        public Task CreateProductTipe(ProductTipe tipe);
        public Task<List<ProductCategory>> GetCategoriesById(int id);
        public Task<bool> CheckCategoryAndTipeIds(List<int> categoryIds, List<int> tipeIds);
        public Task<List<ProductTipe>> GetTipesById(int id);
        public Task AddProductImage(ProductImage productImage);
    }
}
