using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Robolain.Application.Exceptions;
using Robolain.Domain.Dtos.ProductDtos;
using Robolain.Domain.Interfaces.Repositories;
using Robolain.Domain.Interfaces.Services;
using Robolain.Domain.Models;
using Robolain.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IValidator<CreateProductDto> _createProductDtoValidator;
        private readonly IValidator<UpdateProductDto> _updateProductDtoValidator;

        private readonly IBaseRepository<ProductCategory> _productCategoryRepository;
        private readonly IBaseRepository<Product> _productRepository;

        public ProductService
            (
            IBaseRepository<Product> _productRepository,
            IBaseRepository<ProductCategory> _productCategoryRepository,
            IValidator<UpdateProductDto> _updateProductDtoValidator,
            IValidator<CreateProductDto> _createProductDtoValidator
            )
        {
            this._productRepository = _productRepository;
            this._productCategoryRepository = _productCategoryRepository;
            this._updateProductDtoValidator = _updateProductDtoValidator;
            this._createProductDtoValidator = _createProductDtoValidator;
        }

        public async Task<BaseResult> AddProduct(CreateProductDto dto)
        {
            var validRes = await _createProductDtoValidator.ValidateAsync(dto);
            if (!validRes.IsValid)
            {
                throw new ValidException(validRes.Errors);
            }

            var category = await _productCategoryRepository.GetAll()
                                                            .FirstOrDefaultAsync(x=>x.Id == dto.CategoryId);

            if(category == null)
            {
                throw new NotFoundException($"{nameof(ProductCategory)} с Id:{dto.CategoryId} не найдено", Domain.ErrorCodes.NotFound);
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = category.Id,
            };

            await _productRepository.CreateAsync(product);

            return new BaseResult
            {
                Message = $"{nameof(Product)} успешно добавлен"
            };
        }

        public async Task<BaseResult> DeleteProduct(int productId)
        {
            var product = await _productRepository.GetAll()
                                               .FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                throw new NotFoundException($"{nameof(Product)} с Id:{productId} не найден", Domain.ErrorCodes.NotFound);
            }

            await _productRepository.DeleteAsync(product);

            return new BaseResult
            {
                Message = $"{nameof(Product)} с Id:{productId} успешно удален"
            };


        }

        public async Task<BaseResult<ProductDto>> GetProduct(int productId)
        {
            var product = await _productRepository.GetAll()
                                                .FirstOrDefaultAsync(x => x.Id == productId);

            if(product == null)
            {
                throw new NotFoundException($"{nameof(Product)} с Id:{productId} не найден", Domain.ErrorCodes.NotFound);
            }

            return new BaseResult<ProductDto>
            {
                //Здесь опять же можно было бы воспользоваться библиотекой Mapper
                Data = new ProductDto { Name = product.Name, Description = product.Description, Price = product.Price },
                Message = $"{nameof(Product)} успешно возвращен!"
            };
        }

        public async Task<BaseResult<List<ProductDto>>> GetProducts()
        {
            var products = await _productRepository.GetAll()
                                                    .Select(x => new ProductDto
                                                    {
                                                        Name = x.Name,
                                                        Description = x.Description,
                                                        Price = x.Price,
                                                    }).ToListAsync();

            if(products.Count == 0)
            {
                return new BaseResult<List<ProductDto>> { Message = $"Список с {nameof(Product)} пуст"};
            }

            return new BaseResult<List<ProductDto>>
            {
                Data = products,
                Message = $"Список с {nameof(Product)} успешно возвращен!"
            };
        }

        public async Task<BaseResult> UpdateProduct(UpdateProductDto dto)
        {
            var validRes = await _updateProductDtoValidator.ValidateAsync(dto);
            if (!validRes.IsValid)
            {
                throw new ValidException(validRes.Errors);
            }

            var product = await _productRepository.GetAll()
                                               .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (product == null)
            {
                throw new NotFoundException($"{nameof(Product)} с Id:{dto.Id} не найден", Domain.ErrorCodes.NotFound);
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;

            await _productRepository.UpdateAsync(product);

            return new BaseResult
            {
                Message = $"{nameof(Product)} с Id:{dto.Id} успешно изменен"
            };
        }
    }
}
