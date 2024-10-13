using Robolain.Domain.Dtos.ProductCategoryDtos;
using Robolain.Domain.Models;
using Robolain.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robolain.Domain.Interfaces.Services
{
    public interface IProductCategoryService
    {
        Task<BaseResult<List<ProductCategoryDto>>> GetAllProductCategories();

        Task<BaseResult> AddProductCategory(CreateProductCategoryDto dto);

        Task<BaseResult> DeleteProductCategory(int productCategoryId);

        Task<BaseResult> UpdateProductCategory(UpdateProductCategoryDto dto);

        Task<BaseResult<ProductCategoryDto>> GetProductCategory(int productCategoryId);

    }
}
