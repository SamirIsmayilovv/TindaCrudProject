using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories.Interfaces
{
    public interface ICategoryReposity:IRepository<Category>
    {
        public Task<IEnumerable<ProductCategory>> GetProductsByCategory(int id);
    }
}
