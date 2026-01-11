using System.Linq.Expressions;
using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.DAL.Entities;

namespace eCommerce.ProductService.BLL.ServicesInterfaces;

public interface IProductsService
{
    Task<ProductResponse<IEnumerable<ProductDto>>> GetProductsAsync(int page = 1, int pageSize = 10);
    Task<ProductResponse<ProductDto>> GetProductAsync(Guid id);
    Task<ProductResponse<IEnumerable<ProductDto>>> GetProductsByIdsAsync(GetProductsByIdsRequest getProductsByIdsRequest);
    Task<ProductResponse<IEnumerable<ProductDto>>> GetProductsByConditionAsync(ProductFilterDto filter);
    Task<ProductResponse<ProductDto>> GetProductByConditionAsync(ProductFilterDto filter);
    Task<ProductResponse<ProductDto>> AddProductAsync(AddProductRequest addProductRequest);
    Task<ProductResponse<ProductDto>> UpdateProductAsync(UpdateProductRequest updateProductRequest);
    Task<ProductResponse<ProductDto>> DeleteProductAsync(Guid id);
    Task<ProductResponse<ProductDto>> UpdateProductStockAsync(Guid id, ReduceStockRequest reduceStockRequest);
}