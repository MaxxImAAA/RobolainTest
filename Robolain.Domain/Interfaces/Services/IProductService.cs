using Robolain.Domain.Dtos.ProductDtos;
using Robolain.Domain.Models;
using Robolain.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<BaseResult> AddProduct(CreateProductDto dto);

        Task<BaseResult<List<ProductDto>>> GetProducts();

        Task<BaseResult<ProductDto>> GetProduct(int productId);

        Task<BaseResult> DeleteProduct(int productId);

        Task<BaseResult> UpdateProduct(UpdateProductDto dto);
    }
}
