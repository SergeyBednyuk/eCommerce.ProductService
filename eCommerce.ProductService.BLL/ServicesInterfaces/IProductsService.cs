using System.Linq.Expressions;
using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.DAL.Entities;

namespace eCommerce.ProductService.BLL.ServicesInterfaces;

public interface IProductsService
{
    Task<IEnumerable<ProductResponse>> GetProductsAsync(int page = 1, int pageSize = 10);
    Task<IEnumerable<ProductResponse>> GetProductsByConditionAsync(Expression<Func<ProductResponse, bool>> conditionExpression);
    Task<ProductResponse> GetProductByConditionAsync(Expression<Func<ProductResponse, bool>> conditionExpression);
    Task<ProductResponse> AddProductAsync(AddProductRequest addProductRequest);
    Task<ProductResponse> UpdateProductAsync(UpdateProductRequest updateProductRequest);
    Task<ProductResponse> DeleteProductAsync(Guid id);
}