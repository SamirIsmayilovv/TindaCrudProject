using Project.Core.Entities;
using Project.Data.DTOs.Tipe;
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
    public class TipeService:ITipeService
    {
        private readonly ITipeRepository _repository;
        public TipeService(ITipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Responsee> CreateAsync(TipePostDto dto)
        {
            Tipe tipe = new()
            {
                Name = dto.Name,
                CreatedDate = DateTime.Now,
            };
            await _repository.CreateAsync(tipe);
            return new Responsee()
            {
                Message = "Created!",
                Data = new TipeGetDto()
                {
                    Name = dto.Name,
                    CreatedTime = $"{DateTime.Now.Day}:{DateTime.Now.Month}:{DateTime.Now.Year}",
                },
                StatusCode = 200
            };
        }

        public async Task<Responsee> GetAllAsync()
        {
            List<Tipe> types = await _repository.GetAllAsync(x => !x.IsDeleted);
            List<TipeGetDto> dtos = types
                .Select(data => new TipeGetDto
                {
                    Name = data.Name,
                    CreatedTime = data.CreatedDate.ToString("dd : MM : yyyy"),
                    Id = data.Id,
                })
                .ToList();
            if (types.Count() != 0)
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
                Message = "There is no Type",
                StatusCode = 204
            };
        }

        public async Task<Responsee> GetAsync(int id)
        {
            Tipe tipe = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (tipe == null)
            {
                return new Responsee()
                {
                    Message = "Type with this ID does not exist!",
                    StatusCode = 404
                };
            }
            return new Responsee()
            {
                Data = new TipeGetDto()
                {
                    Name = tipe.Name,
                    CreatedTime = tipe.CreatedDate.ToString("dd : MM : yyyy")
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
            Tipe tipe = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            await _repository.HardDeleteAsync(tipe);
            return new Responsee()
            {
                Message = $"Category named {tipe.Name} Succesfully Deleted. Compleately!",
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
            Tipe tipe = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            tipe.IsDeleted = true;
            await _repository.SaveChangesAsync();
            return new Responsee()
            {
                Message = $"Category named {tipe.Name} Succesfully Deleted",
                StatusCode = 200
            };
        }

        public async Task<Responsee> UpdateAsync(int id, TipePutDto dto)
        {
            Tipe type = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (!await _repository.IsExist(x => !x.IsDeleted && x.Id == id))
            {
                return new Responsee()
                {
                    Message = "Type with this ID does not exist!",
                    StatusCode = 404
                };
            }
            string initialName = type.Name;
            type.Name = dto.Name;
            await _repository.SaveChangesAsync();
            return new Responsee()
            {
                Message = $"category name has been changed from {initialName}, to {type.Name}",
                StatusCode = 200
            };
        }

        public async Task<Responsee> GetProductsByType(int id)
        {
            if (!await _repository.IsExist(x => !x.IsDeleted && x.Id == id))
                return new Responsee() { Message = "There is no such a type with this ID", StatusCode = 404 };
            IEnumerable<ProductTipe> products = await _repository.GetProductsByTypeId(id);
            if (products.Count() == 0)
                return new Responsee() { Message = "There is no product in this type", StatusCode = 204 };
            List<string> names = new List<string>();
            foreach (var item in products)
            {
                names.Add(item.Product.Name);
            }
            return new Responsee()
            {
                StatusCode = 200,
                Data = names
            };
        }

    }
}
