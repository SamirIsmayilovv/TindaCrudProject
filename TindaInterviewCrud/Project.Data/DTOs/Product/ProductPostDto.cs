using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.DTOs.Product
{
    public class ProductPostDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int NuberInStock { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> TypeIds { get; set; }
        public List<IFormFile>? files { get;set; }
    }
}
