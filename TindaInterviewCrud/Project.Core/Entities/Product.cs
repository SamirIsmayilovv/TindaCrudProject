using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Entities
{
    public class Product:BaseModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int NumberInStock { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public List<ProductTipe> ProductTipes { get; set;}
        public List<ProductImage> ProductImages { get; set; }
    }
}
