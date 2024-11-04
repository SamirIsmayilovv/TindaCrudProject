using Project.Core.Entities;
using Project.Data.DTOs.Category;
using Project.Data.DTOs.Tipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.DTOs.Product
{
    public class ProductGetDto
    {
        public string Name { get; set; }
        public List<CategoryGetDto> Categories { get; set; }
        public List<TipeGetDto> Tipes { get; set; }
        public List<ProductImage> Images { get; set; }
        public string ImageLink { get; set; }
    }
}
