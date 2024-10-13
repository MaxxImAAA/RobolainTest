using FluentValidation;
using Robolain.Domain.Dtos.ProductCategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Application.Validation.ProductCategoryValidation
{
    public class CreateProductCategoryValidator : AbstractValidator<CreateProductCategoryDto>
    {
        public CreateProductCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3).WithMessage("Минимальная длина 3 символа!")
                .MaximumLength(15).WithMessage("Максимальная длина 15 символов!");
        }
    }
}
