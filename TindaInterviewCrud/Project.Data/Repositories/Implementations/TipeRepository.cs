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
    public class TipeRepository:Repository<Tipe> ,ITipeRepository
    {
        private readonly AppDbcontext _context;
        public TipeRepository(AppDbcontext context) : base(context)
        {
            _context=context;
        }
        public async Task<IEnumerable<ProductTipe>> GetProductsByTypeId(int id)
        {
            return await _context.ProductTipes
               .Where(x => !x.IsDeleted && x.TipeId == id)
               .Include(x => x.Product)
               .ToListAsync();
        }
    }
}
