using FluentValidation;
using Robolain.Domain.Dtos.ProductCategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Application.Validation.ProductCategoryValidation
{
    public class UpdateProductCategoryValidator : AbstractValidator<UpdateProductCategoryDto>
    {
        public UpdateProductCategoryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id должен быть заполнен");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name должен быть заполнен")
                .Matches(@"^[^\d]+$").WithMessage("Name не должен содержать цифр");


        }
    }
}
