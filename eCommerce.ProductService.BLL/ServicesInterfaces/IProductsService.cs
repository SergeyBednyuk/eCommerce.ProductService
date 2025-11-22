using System.Linq.Expressions;
using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.DAL.Entities;

namespace eCommerce.ProductService.BLL.ServicesInterfaces;

public interface IProductsService
{
    Task<ProductResponse<ProductDto>> GetProductsAsync(int page = 1, int pageSize = 10);

    Task<ProductResponse<ProductDto>> GetProductsByConditionAsync(
        Expression<Func<ProductResponse<ProductDto>, bool>> conditionExpression);

    Task<ProductResponse<ProductDto>> GetProductByConditionAsync(
        Expression<Func<ProductResponse<ProductDto>, bool>> conditionExpression);

    Task<ProductResponse<ProductDto>> AddProductAsync(AddProductRequest addProductRequest);
    Task<ProductResponse<ProductDto>> UpdateProductAsync(UpdateProductRequest updateProductRequest);
    Task<ProductResponse<ProductDto>> DeleteProductAsync(Guid id);
}