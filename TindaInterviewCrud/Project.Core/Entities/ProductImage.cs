using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Entities
{
    public class ProductImage:BaseModel
    {
        public string Image { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsMain { get; set; }
        public string Base64Image { get; set; }
    }
}
