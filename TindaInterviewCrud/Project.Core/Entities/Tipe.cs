using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Entities
{
    public class Tipe:BaseModel
    {
        public string Name { get; set; }
        public List<ProductTipe> ProductTipes { get; set; }
    }
}
