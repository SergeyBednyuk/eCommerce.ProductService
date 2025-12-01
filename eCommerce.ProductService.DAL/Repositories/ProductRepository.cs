using System.Linq.Expressions;
using eCommerce.ProductService.DAL.Context;
using eCommerce.ProductService.DAL.Entities;
using eCommerce.ProductService.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.ProductService.DAL.Repositories;

public class ProductRepository(ApplicationDbContext dbContext) : IProductsRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber, int pageSize)
    {
        var result = await _dbContext.Products
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Product>> GetProductsByConditionAsync(
        Expression<Func<Product, bool>> conditionExpression)
    {
        var result = await _dbContext.Products.AsNoTracking().Where(conditionExpression).ToListAsync();
        return result;
    }

    public async Task<Product?> GetProductByConditionAsync(Expression<Func<Product, bool>> conditionExpression)
    {
        var result = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(conditionExpression);
        return result;
    }

    public async Task<Product?> AddProductAsync(Product product)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateProductAsync(Product product)
    {
        var productFromDb = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
        if (productFromDb is null) return null;

        //questionable approach
        //good for performance but will be mess if the object has lots of properties
        // productFromDb.ProductName = product.ProductName;
        // productFromDb.Category = product.Category;
        // productFromDb.QuantityInStock = product.QuantityInStock;
        // productFromDb.UnitPrice = product.UnitPrice;

        _dbContext.Entry(productFromDb).CurrentValues.SetValues(product);
        await _dbContext.SaveChangesAsync();
        return productFromDb;
    }

    public async Task<bool> DeleteProductAsync(Guid productId)
    {
        var affectedRowsCount = await _dbContext.Products
            .Where(x => x.Id == productId)
            .ExecuteDeleteAsync();
        return affectedRowsCount > 0;
    }
    
}