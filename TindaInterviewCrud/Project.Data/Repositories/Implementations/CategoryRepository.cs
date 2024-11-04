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
    public class CategoryRepository:Repository<Category>,ICategoryReposity
    {
        private readonly AppDbcontext _context;
        public CategoryRepository(AppDbcontext context):base(context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductCategory>> GetProductsByCategory(int id)
        {
            return await _context.ProductCategories
               .Where(x => !x.IsDeleted && x.CategoryId == id)
               .Include(x => x.Product)
               .ToListAsync();
        }

    }
}
