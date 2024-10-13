using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Robolain.Application.Exceptions;
using Robolain.Domain.Dtos.ProductCategoryDtos;
using Robolain.Domain.Interfaces.Services;
using Robolain.Domain.Models;
using Robolain.Domain.Results;

namespace Robolain.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IValidator<CreateProductCategoryDto> _createProductCategoryDtovalidator;
        private readonly IProductCategoryService _productCategoryService;
        public ProductCategoryController
            (
            IProductCategoryService _productCategoryService,
            IValidator<CreateProductCategoryDto> _createProductCategoryDtovalidator
            )
        {
            this._productCategoryService = _productCategoryService;
            this._createProductCategoryDtovalidator = _createProductCategoryDtovalidator;
        }

        [HttpGet("GetCategoryProducts")]
        public async Task<ActionResult<BaseResult<List<ProductCategoryDto>>>> GetCategoryProducts()
        {
            var response = await _productCategoryService.GetAllProductCategories();
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("AddCategoryProduct")]
        public async Task<ActionResult<BaseResult>> AddCategoryProduct(CreateProductCategoryDto dto)
        {
          
            var response = await _productCategoryService.AddProductCategory(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpDelete("DeleteCategoryProduct")]
        public async Task<ActionResult<BaseResult>> DeleteProductCategory(int productCategoryId)
        {
           
            var response = await _productCategoryService.DeleteProductCategory(productCategoryId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetCategoryProduct")]
        public async Task<ActionResult<BaseResult<ProductCategoryDto>>> GetProductCategory(int productCategoryId)
        {
            var response = await _productCategoryService.GetProductCategory(productCategoryId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpPatch("UpdateCategoryProduct")]
        public async Task<ActionResult<BaseResult>> UpdateProductCategory(UpdateProductCategoryDto dto)
        {
            var response = await _productCategoryService.UpdateProductCategory(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
