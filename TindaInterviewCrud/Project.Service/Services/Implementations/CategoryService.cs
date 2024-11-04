using Azure;
using Project.Core.Entities;
using Project.Data.DTOs.Category;
using Project.Data.Repositories.Interfaces;
using Project.Service.Reponses;
using Project.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryReposity _repository;
        public CategoryService(ICategoryReposity repository)
        {
            _repository = repository;
        }

        public async Task<Responsee> CreateAsync(CategoryPostDto dto)
        {
            Category category = new()
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now,
            };
            await _repository.CreateAsync(category);
            return new Responsee()
            {
                Message = "Created!",
                Data = new CategoryGetDto()
                {
                    Name = dto.Name,
                    CreatedTime = $"{DateTime.Now.Day}:{DateTime.Now.Month}:{DateTime.Now.Year}",
                },
                StatusCode = 200
            };
        }

        public async Task<Responsee> GetAllAsync()
        {
            List<Category> categories = await _repository.GetAllAsync(x => !x.IsDeleted);
            List<CategoryGetDto> dtos = categories
                .Select(data => new CategoryGetDto
                {
                    Name = data.Name,
                    CreatedTime = data.CreatedDate.ToString("dd : MM : yyyy"),
                    Id = data.Id,
                })
                .ToList();
            if (categories.Count() != 0)
            {
                return new Responsee()
                {
                    Message = "Here you go!",
                    Data = dtos,
                    StatusCode = 200
                };
            }
            return new Responsee()
            {
                Message = "There is no Category",
                StatusCode = 204
            };
        }

        public async Task<Responsee> GetAsync(int id)
        {
            Category category = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (category == null)
            {
                return new Responsee()
                {
                    Message = "Category with this ID does not exist!",
                    StatusCode = 404
                };
            }
            return new Responsee()
            {
                Data = new CategoryGetDto()
                {
                    Name = category.Name,
                    CreatedTime = category.CreatedDate.ToString("dd : MM : yyyy")
                },
                StatusCode = 200,
            };
        }

        public async Task<Responsee> HardDeleteAsync(int id)
        {
            if (!await _repository.IsExist(x => !x.IsDeleted && x.Id == id))
            {
                return new Responsee()
                {
                    Message = "Category with such an ID does not exist!",
                    StatusCode = 404,
                };
            }
            Category cat = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            await _repository.HardDeleteAsync(cat);
            return new Responsee()
            {
                Message = $"Category named {cat.Name} Succesfully Deleted. Compleately!",
                StatusCode = 200
            };
        }

        public async Task<Responsee> SoftDeleteAsync(int id)
        {
            if (!await _repository.IsExist(x => !x.IsDeleted && x.Id == id))
            {
                return new Responsee()
                {
                    Message = "Category with such an ID does not exist!",
                    StatusCode = 404,
                };
            }
            Category cat = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            cat.IsDeleted = true;
            await _repository.SaveChangesAsync();
            return new Responsee()
            {
                Message = $"Category named {cat.Name} Succesfully Deleted",
                StatusCode = 200
            };
        }

        public async Task<Responsee> UpdateAsync(int id, CategoryPutDto dto)
        {
            Category cat = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            string initialName = cat.Name;
            cat.Name = dto.Name;
            await _repository.SaveChangesAsync();
            return new Responsee()
            {
                Message = $"category name has been changed from {initialName}, to {cat.Name}",
                StatusCode = 200
            };
        }
        public async Task<Responsee> GetProductsByCategory(int id)
        {
            if (!await _repository.IsExist(x => !x.IsDeleted && x.Id == id))
                return new Responsee() { Message = "There is no such a category with this ID", StatusCode = 404 };
            IEnumerable<ProductCategory> products = await _repository.GetProductsByCategory(id);
            if (products.Count() == 0)
                return new Responsee() { Message = "There is no product in this category", StatusCode = 204 };
            List<string> names = new List<string>();
            foreach (var item in products)
            {
                names.Add(item.Product.Name);
            }
            return new Responsee()
            {
                StatusCode=200,
                Data = names
            };
        }

    }
}
