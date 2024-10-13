using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Robolain.Application.Services;
using Robolain.Application.Validation.ProductCategoryValidation;
using Robolain.Domain.Dtos.ProductCategoryDtos;
using Robolain.Domain.Dtos.ProductDtos;
using Robolain.Domain.Interfaces.Services;
using Robolain.Application.Validation.ProductValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;

namespace Robolain.Application.DependencyInjections
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {

            AddServices(services);
            AddValidator(services);

          
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();
        }

        private static void AddValidator(IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateProductCategoryDto>, CreateProductCategoryValidator>();
            services.AddScoped<IValidator<CreateProductDto>, CreateProductValidator>();
            services.AddScoped<IValidator<UpdateProductCategoryDto>, UpdateProductCategoryValidator>();
            services.AddScoped<IValidator<UpdateProductDto>, UpdateProductValidator>();
        }
    }
}
