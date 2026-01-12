using System.Linq.Expressions;
using eCommerce.ProductService.DAL.Entities;

namespace eCommerce.ProductService.DAL.RepositoryInterfaces;
/// <summary>
/// Represents a repository for managing 'products' table
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    /// Get All products(with pagination)
    /// </summary>
    /// <returns>All Products</returns>
    Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize);
    /// <summary>
    /// Get all products based on condition
    /// </summary>
    /// <param name="conditionExpression">filtering condition</param>
    /// <returns>Products collection based on condition</returns>
    Task<IEnumerable<Product>> GetProductsByConditionAsync(Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// Get product based on condition
    /// </summary>
    /// <param name="conditionExpression">filtering condition</param>
    /// <returns>Product based on condition</returns>
    Task<Product?> GetProductByConditionAsync(Expression<Func<Product, bool>> conditionExpression);
    /// <summary>
    /// Get product for update(without AsNoTracking) based on condition
    /// </summary>
    /// <param name="id">product id</param>
    /// <returns>Product based on condition</returns>
    Task<Product?> GetProductForUpdateAsync(Guid id);

    /// <summary>
    /// Get products for update(without AsNoTracking) based on condition
    /// </summary>
    /// <param name="ids">Products ids</param>
    /// <returns>Product based on condition</returns>
    Task<IEnumerable<Product>> GetProductsForUpdateAsync(IEnumerable<Guid> ids);
    /// <summary>
    /// Add new product
    /// </summary>
    /// <param name="product">Product that should be added</param>
    /// <returns>New project or null</returns>
    Task<Product?> AddProductAsync(Product product);
    /// <summary>
    /// Update existing product
    /// </summary>
    /// <param name="product">product that should be updated</param>
    /// <returns>updated product</returns>
    Task<Product?> UpdateProductAsync(Product product);
    /// <summary>
    /// Delete the product by id
    /// </summary>
    /// <param name="productId">product id that should be deleted</param>
    /// <returns>true if deleting was successful or false if it was not</returns>
    Task<bool> DeleteProductAsync(Guid productId);
    /// <summary>
    /// To support of Unit of works for Products repo
    /// </summary>
    /// <returns></returns>
    public Task SaveProductsChangesAsync();
}