using FluentValidation;
using Robolain.Domain.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Application.Validation.ProductValidation
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя не должно быть пустым")
                .Matches(@"^[^\d]+$").WithMessage("Имя не должно содержать цифр");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описание не должно быть пустым");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Цена не должна быть пустой");

            RuleFor(x=>x.CategoryId)
                .NotEmpty().WithMessage("Поле должно содержать внешний ключ на категорию");

        }
    }
}
