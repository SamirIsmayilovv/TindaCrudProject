using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Entities
{
    public class ProductTipe:BaseModel
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int TipeId { get; set; } 
        public Tipe Tipe { get; set; }
    }
}
