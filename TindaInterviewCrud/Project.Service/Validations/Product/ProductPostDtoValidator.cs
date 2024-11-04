using FluentValidation;
using Project.Data.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Validations.Product
{
    public class ProductPostDtoValidator:AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name must be entered")
                .NotNull().WithMessage("Name cannot be null");

            RuleFor(x => x.CategoryIds)
                .NotNull().WithMessage("At least one category must be entered to create the product!")
                .Must(ids => ids.Any()).WithMessage("At least one category must be entered to create the product!");

            RuleFor(x => x.TypeIds)
                .NotNull().WithMessage("At least one type must be entered to create the product!")
                .Must(ids => ids.Any()).WithMessage("At least one type must be entered to create the product!");
        }
    }
}
