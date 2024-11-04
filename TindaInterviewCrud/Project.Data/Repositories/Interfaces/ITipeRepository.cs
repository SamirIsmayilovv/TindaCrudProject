using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Repositories.Interfaces
{
    public interface ITipeRepository:IRepository<Tipe>
    {
        public Task<IEnumerable<ProductTipe>> GetProductsByTypeId(int id);
    }
}
