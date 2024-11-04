using FluentValidation;
using Project.Data.DTOs.Category;
using Project.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Data.Repositories.Interfaces;

namespace Project.Service.Validations.Category
{
    public class CategoryPostDtoValidator : AbstractValidator<CategoryPostDto>
    {

        private readonly ICategoryReposity _repository;        

        public CategoryPostDtoValidator(ICategoryReposity reposity)
        {
            _repository = reposity;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must be entered!")
                .NotNull().WithMessage("Name cannot be null!")
                .MustAsync(async (name, cancellation) => !await _repository.IsExist(x => x.Name == name))
                .WithMessage(name => $"Category with name {name} already exists!");
        }
    }
}
