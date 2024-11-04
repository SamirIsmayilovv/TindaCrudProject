using Microsoft.EntityFrameworkCore;
using Project.Core.Entities;
using Project.Data.Contexts;
using Project.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories.Implementations
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {
        private readonly AppDbcontext _context;
        public ProductRepository(AppDbcontext context):base(context) 
        {
            _context = context;
        }
        public async Task CreateProductCategory(ProductCategory product)
        {
            await _context.ProductCategories.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task CreateProductTipe(ProductTipe tipe)
        {
            await _context.ProductTipes.AddAsync(tipe);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductCategory>> GetCategoriesById(int id)
        {
            return await _context.ProductCategories
                .Where(x => !x.IsDeleted && x.ProductId == id)
                .Include(x=>x.Category)
                .ToListAsync();
        }

        public async Task<bool> CheckCategoryAndTipeIds(List<int> categoryIds, List<int> tipeIds)
        {
            bool categoryIdsExist = !await _context.Categories
                .Where(category => !category.IsDeleted)
                .AnyAsync(category => categoryIds.Contains(category.Id) == false);

            if (!categoryIdsExist)
                return false;

            bool tipeIdsExist = !await _context.ProductTipes
                .Where(tipe => !tipe.IsDeleted)
                .AnyAsync(tipe => tipeIds.Contains(tipe.Id) == false);

            return tipeIdsExist;
        }


        public async Task<List<ProductTipe>> GetTipesById(int id)
        {
            return await _context.ProductTipes
                .Where(x => !x.IsDeleted && x.ProductId == id)
                .Include(x => x.Tipe)
                .ToListAsync();
        }

        public async Task AddProductImage(ProductImage productImage)
        {
            await _context.ProductImages.AddAsync(productImage);
            await _context.SaveChangesAsync();
        }



    }
}
