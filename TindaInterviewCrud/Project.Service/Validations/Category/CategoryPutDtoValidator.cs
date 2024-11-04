using FluentValidation;
using Project.Data.DTOs.Category;
using Project.Data.Repositories.Implementations;
using Project.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Validations.Category
{
    public class CategoryPutDtoValidator:AbstractValidator<CategoryPutDto>
    {
        private readonly ICategoryReposity _repository;
        public CategoryPutDtoValidator(ICategoryReposity repository)
        {
            _repository = repository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must be entered!")
                .NotNull().WithMessage("Name cannot be null!")
                .MustAsync(async (name, cancellation) => !await _repository.IsExist(x => x.Name == name))
                .WithMessage(name => $"Category with name {name} already exists!");

        }
    }
}
