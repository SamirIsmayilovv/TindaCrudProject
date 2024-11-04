using Microsoft.AspNetCore.Hosting;
using Project.Core.Entities;
using Project.Data.DTOs.Product;
using Project.Data.Repositories.Interfaces;
using Project.Service.Extentions;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Implementations
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _repository;
        private readonly IWebHostEnvironment _enviroment;

        public ProductService(IProductRepository repository,IWebHostEnvironment enviroment)
        {
            _repository = repository;
            _enviroment= enviroment;

        }


        public async Task<Responsee> CreateAsync(ProductPostDto dto)
        {
            Product product = new()
            {
                Name = dto.Name,
                NumberInStock=dto.NuberInStock,
                Price = dto.Price,
            };

            if (dto.files.Count() == 0)
                return new Responsee() { Message = "One image must be entered at least!", StatusCode = 400 };
            int counter = 0;
            //atılan filelarin ölçüsünü və şəkil olub olmamasını yoxlamaq 

            if (dto.files.Any(x => x.IsSizeOk(5)) || dto.files.Any(x=>x.IsImage()))         
                return new Responsee() { Message = "Size must be min 5 and file must be an image!", StatusCode = 400 };
          
            //şəkil fileların yaradılması
            foreach (var item in dto.files)
            {
                string imagePath = item.CreateImage(_enviroment.WebRootPath, "Assets/Images");
                string base64Image = await item.ToBase64StringAsync();

                ProductImage image = new()
                {
                    Image = imagePath,
                    Product = product,
                    IsMain = counter == 0,
                    Base64Image = base64Image
                };
                counter++;
                await _repository.AddProductImage(image);
            }

            await _repository.CreateAsync(product);

            if(!await _repository.CheckCategoryAndTipeIds(dto.CategoryIds, dto.TypeIds))
            {
                return new Responsee() { Message = "One of the ids for type or category does not exist!" ,StatusCode=404};
            }
            foreach (var item in dto.CategoryIds)
            {
                ProductCategory productCategory = new()
                {
                    Product = product,
                    CategoryId = item
                };
                await _repository.CreateProductCategory(productCategory);
            }
            foreach (var item in dto.TypeIds)
            {
                ProductTipe tipe = new()
                {
                    Product = product,
                    TipeId = item
                };
                await _repository.CreateProductTipe(tipe);
            }
            await _repository.SaveChangesAsync();

            return new Responsee()
            {
                Message = "Created",
                StatusCode = 200,
                Data = new ProductGetDto() { Name = product.Name }
            };
        }


        public async Task<Responsee> DeleteAsync(int id)
        {
            Product? product = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (product == null)
                return new Responsee() { Message = "Not Found", StatusCode = 404 };
            await _repository.HardDeleteAsync(product); 
            return new Responsee() { StatusCode= 200,Message="Deleted!" };
        }

        public async Task<Responsee> GetAllAsync()
        {
            IEnumerable<Product> products = await _repository.GetAllAsync(x => !x.IsDeleted);
            if (products.Count() == 0)
            {
                return new Responsee() { Message = "There is no left!", StatusCode = 404 };
            }
            List<ProductGetDto> dtos = new List<ProductGetDto>();
            foreach (var item in products)
            {
                dtos.Add(new ProductGetDto() { Name = item.Name,Images=item.ProductImages });
            }

            return new Responsee() { StatusCode = 200, Data = products };
        }

        public async Task<Responsee> GetAsync(int id)
        {
            Product product= await _repository.GetAsync(x=>!x.IsDeleted && x.Id==id);
            if (product == null)
                return new Responsee() { Message = "Not Found", StatusCode = 404 };
            return new Responsee() { Data=new ProductGetDto() { Name=product.Name},StatusCode=200 };
        }

        public async Task<Responsee> UpdateAsync(ProductPutDto dto)
        {
            Product product = await _repository.GetAsync(x => !x.IsDeleted && x.Id == dto.ProdcutId);
            if (product == null)
                return new Responsee() { Message = "product with this id does not exist!" ,StatusCode=404};
            product.Name = dto.Name; product.Price = dto.Price ?? product.Price;
            await _repository.UpdateAsync(product);
            return new Responsee() { Message = "Updated Succesfully!", StatusCode =200};
        }

        public async Task<Responsee> GetCategoriesByProductId(int id)
        {
            if (!await _repository.IsExist(x => !x.IsDeleted && x.Id == id))
                return new Responsee() { Message = "Product with such an id does not xist!", StatusCode = 404 };
            List<ProductCategory> categories = await _repository.GetCategoriesById(id);
            List<string> names = new List<string>();
            foreach (ProductCategory category in categories)
            {
                names.Add(category.Category.Name);
            }
            return new Responsee()
            {
                StatusCode=200,
                Data=names
            };
        }

        public async Task<Responsee> GetTypesByProductId(int id)
        {
            if (!await _repository.IsExist(x => !x.IsDeleted && x.Id == id))
                return new Responsee() { Message = "Product with such an id does not xist!", StatusCode = 404 };
            List<ProductTipe> tipes = await _repository.GetTipesById(id);
            List<string> names = new List<string>();
            foreach (ProductTipe category in tipes)
            {
                names.Add(category.Tipe.Name);
            }
            return new Responsee()
            {
                StatusCode = 200,
                Data = names
            };
        }
    }
}
