using Microsoft.EntityFrameworkCore;
using Project.Data.Contexts;
using Project.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories.Implementations
{
    public class Repository<W> : IRepository<W> where W : class
    {
        private readonly AppDbcontext _context;
        public Repository(AppDbcontext context)
        {
            _context = context;
        }

        public async Task CreateAsync(W entity)
        {
            await _context.Set<W>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<W>> GetAllAsync(Expression<Func<W, bool>> expression)
        {
            return await _context.Set<W>().Where(expression).ToListAsync();
        }

        public async Task<W> GetAsync(Expression<Func<W, bool>> expression)
        {
            return await _context.Set<W>().Where(expression).FirstOrDefaultAsync();
        }

        public async Task<bool> IsExist(Expression<Func<W, bool>> expression)
        {
            object? data = await _context.Set<W>().Where(expression).FirstOrDefaultAsync();
            if (data != null)
                return true;
            return false;
        }

        public async Task Remove(W entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(W entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task HardDeleteAsync(W entity)
        {
            _context.Set<W>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
