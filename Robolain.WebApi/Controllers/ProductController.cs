using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Robolain.Domain.Dtos.ProductDtos;
using Robolain.Domain.Interfaces.Services;
using Robolain.Domain.Models;
using Robolain.Domain.Results;

namespace Robolain.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<BaseResult<List<ProductDto>>>> GetProducts()
        {
            var response = await _productService.GetProducts();
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetProduct")]
        public async Task<ActionResult<BaseResult<ProductDto>>> GetProduct(int productId)
        {
            var response = await _productService.GetProduct(productId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<BaseResult>> AddProduct(CreateProductDto dto)
        {
            var response = await _productService.AddProduct(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<BaseResult>> DeleteProduct(int productId)
        {
            var response = await _productService.DeleteProduct(productId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPatch("UpdateProduct")]
        public async Task<ActionResult<BaseResult>> UpdateProduct(UpdateProductDto dto)
        {
            var response = await _productService.UpdateProduct(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
