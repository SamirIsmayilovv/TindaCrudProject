using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories.Interfaces
{
    public interface IRepository<W>
    {
        public Task CreateAsync(W entity);
        public Task UpdateAsync(W entity);
        public Task<List<W>> GetAllAsync(Expression<Func<W, bool>> expression);
        public Task<W> GetAsync(Expression<Func<W, bool>> expression);
        public Task Remove(W entity);
        public Task<bool> IsExist(Expression<Func<W, bool>> expression);
        public Task SaveChangesAsync();
        public Task HardDeleteAsync(W entity);
    }
}
