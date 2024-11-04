using FluentValidation;
using Project.Data.DTOs.Tipe;
using Project.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Validations.Tipe
{
    public class TipePutDtoValidator:AbstractValidator<TipePutDto>
    {
        private readonly ITipeRepository _repository;
        public TipePutDtoValidator(ITipeRepository repository)
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
