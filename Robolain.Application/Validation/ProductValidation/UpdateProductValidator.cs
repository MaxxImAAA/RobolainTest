using FluentValidation;
using Robolain.Domain.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Application.Validation.ProductValidation
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id не должен быть пустым");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name не должен быть пустым")
                .Matches(@"^[^\d]+$").WithMessage("Name не должен содержать цифр");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description не должен быть пустым")
                .MinimumLength(10).WithMessage("Минимальная длина Description - 10")
                .MaximumLength(50).WithMessage("Максимальная длина Description - 50");
        }
    }
}
