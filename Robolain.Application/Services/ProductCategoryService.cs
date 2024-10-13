using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Robolain.Application.Exceptions;
using Robolain.Domain.Dtos.ProductCategoryDtos;
using Robolain.Domain.Interfaces.Repositories;
using Robolain.Domain.Interfaces.Services;
using Robolain.Domain.Models;
using Robolain.Domain.Results;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robolain.Application.Exceptions;

namespace Robolain.Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IValidator<CreateProductCategoryDto> _createProductCategoryDtoValidator;
        private readonly IValidator<UpdateProductCategoryDto> _updateProductCategoryDtoValidator;

        private readonly IBaseRepository<ProductCategory> _productCategoryRepository;
        public ProductCategoryService
            (
            IBaseRepository<ProductCategory> _productCategoryRepository,
             IValidator<CreateProductCategoryDto> _createProductCategoryDtoValidator, 
             IValidator<UpdateProductCategoryDto> _updateProductCategoryDtoValidator
            )
        {
            this._productCategoryRepository = _productCategoryRepository;
            this._createProductCategoryDtoValidator = _createProductCategoryDtoValidator;
            this._updateProductCategoryDtoValidator = _updateProductCategoryDtoValidator;
        }

        public async Task<BaseResult> AddProductCategory(CreateProductCategoryDto dto)
        {
            var validRes = await _createProductCategoryDtoValidator.ValidateAsync(dto);
            if (!validRes.IsValid)
            {
                throw new ValidException(validRes.Errors);
            }


            if (await _productCategoryRepository.GetAll().FirstOrDefaultAsync(x=>x.Name == dto.Name) != null)
            {
                throw new NameExistsException($"Имя для создаваемого {nameof(ProductCategory)} уже занято!",
                    Domain.ErrorCodes.NameExists);
            }

            var productCategory = new ProductCategory
            {
                Name = dto.Name,
                Products = new List<Product>()
            };

            await _productCategoryRepository.CreateAsync(productCategory);

            return new BaseResult()
            {
                Message = "Категория продуктов успешно добавлена"
            };
                
        }

        public async Task<BaseResult> DeleteProductCategory(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetAll()
                                                                  .FirstOrDefaultAsync(x=>x.Id == productCategoryId);
            if(productCategory == null)
            {
                throw new NotFoundException($"{nameof(ProductCategory)} с Id:{productCategoryId} не найден!",
                    Domain.ErrorCodes.NotFound);
            }

            await _productCategoryRepository.DeleteAsync(productCategory);

            return new BaseResult
            {
                Message = $"{nameof(ProductCategory)} с Id:{productCategoryId} Удален!"
            };
        }

        public async Task<BaseResult<List<ProductCategoryDto>>> GetAllProductCategories()
        {
            var productCategoryDtos = await _productCategoryRepository.GetAll()
                                                                      .Select(x => new ProductCategoryDto { Id = x.Id, Name = x.Name })
                                                                      .ToListAsync();

            if(productCategoryDtos.Count == 0)
            {
                return new BaseResult<List<ProductCategoryDto>>
                {
                    Message = $"Список с {nameof(ProductCategory)} пуст"
                };
            }

            return new BaseResult<List<ProductCategoryDto>>
            {
                Data = productCategoryDtos,
                Message = $"Все {nameof(ProductCategory)} возвращены!"
            };
        }

        public async Task<BaseResult<ProductCategoryDto>> GetProductCategory(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetAll()
                                                                 .FirstOrDefaultAsync(x => x.Id == productCategoryId);
            if (productCategory == null)
            {
                throw new NotFoundException($"{nameof(ProductCategory)} с Id:{productCategoryId} не найден!",
                    Domain.ErrorCodes.NotFound);
            }

            return new BaseResult<ProductCategoryDto>
            {
                // Здесь можно было бы использовать mapper, но так как полей мало сделал мап руками :)
                Data = new ProductCategoryDto { Id = productCategory.Id, Name = productCategory.Name },
                Message = $"{nameof(productCategory)} Успешно возвращен!"
            };
        }

        public async Task<BaseResult> UpdateProductCategory(UpdateProductCategoryDto dto)
        {
            var validRes = await _updateProductCategoryDtoValidator.ValidateAsync(dto);
            if (!validRes.IsValid)
            {
                throw new ValidException(validRes.Errors);
            }

            var productCategory = await _productCategoryRepository.GetAll()
                                                                 .FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (productCategory == null)
            {
                throw new NotFoundException($"{nameof(ProductCategory)} с Id:{dto.Id} не найден!",
                    Domain.ErrorCodes.NotFound);
            }

            productCategory.Name = dto.Name;

            await _productCategoryRepository.UpdateAsync(productCategory);

            return new BaseResult
            {
                Message = "Редактирование успешно завершено!"
            };
        }
    }
}
