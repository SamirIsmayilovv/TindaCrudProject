using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.DTOs.Product
{
    public class ProductPutDto
    {
        public int ProdcutId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public List<int>? CategoryIds { get; set; }
        public List<int>? TipeIds { get; set; }
        public int UpdatedProductId { get; set; }
        public IFormFile? file { get; set; }
    }
}
