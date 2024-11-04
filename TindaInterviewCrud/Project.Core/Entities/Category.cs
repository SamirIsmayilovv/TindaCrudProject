using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Entities
{
    public class Category:BaseModel
    {
        public string Name { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
