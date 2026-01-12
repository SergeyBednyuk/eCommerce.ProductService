using System.Linq.Expressions;
using AutoMapper;
using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.BLL.ServicesInterfaces;
using eCommerce.ProductService.DAL.Entities;
using eCommerce.ProductService.DAL.RepositoryInterfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.ProductService.BLL.Services;

public class ProductsService(
    IProductsRepository productsRepository,
    IMapper mapper,
    IValidator<AddProductRequest> addProductRequestValidator,
    IValidator<UpdateProductRequest> updateProductRequestValidator,
    IValidator<GetProductsByIdsRequest> getProductsByIdsRequestValidator) : IProductsService
{
    private readonly IProductsRepository _productsRepository = productsRepository;
    private readonly IMapper _mapper = mapper;

    //Validators
    private readonly IValidator<AddProductRequest> _addProductRequestValidator = addProductRequestValidator;
    private readonly IValidator<UpdateProductRequest> _updateProductRequestValidator = updateProductRequestValidator;

    private readonly IValidator<GetProductsByIdsRequest> _getProductsByIdsRequestValidator =
        getProductsByIdsRequestValidator;

    public async Task<ProductResponse<IEnumerable<ProductDto>>> GetProductsAsync(int page = 1, int pageSize = 10)
    {
        var result = await _productsRepository.GetProductsAsync(page, pageSize);
        var mapperResult = _mapper.Map<IEnumerable<ProductDto>>(result);

        return ProductResponse<IEnumerable<ProductDto>>.Success(mapperResult);
    }

    public async Task<ProductResponse<ProductDto>> GetProductAsync(Guid id)
    {
        var result = await _productsRepository.GetProductByConditionAsync(x => x.Id == id);

        if (result is null) return ProductResponse<ProductDto>.Failure("Product not found");

        return ProductResponse<ProductDto>.Success(_mapper.Map<ProductDto>(result));
    }

    public async Task<ProductResponse<IEnumerable<ProductDto>>> GetProductsByIdsAsync(
        GetProductsByIdsRequest getProductsByIdsRequest)
    {
        var validationResult = await _getProductsByIdsRequestValidator.ValidateAsync(getProductsByIdsRequest);
        if (!validationResult.IsValid)
        {
            return ProductResponse<IEnumerable<ProductDto>>.Failure("there are no ids in request",
                validationResult.Errors.Select(x => x.ErrorMessage));
        }

        var result =
            await _productsRepository.GetProductsByConditionAsync(x => getProductsByIdsRequest.Ids.Contains(x.Id));
        var mapperResult = _mapper.Map<IEnumerable<ProductDto>>(result);
        return ProductResponse<IEnumerable<ProductDto>>.Success(mapperResult);
    }

    public async Task<ProductResponse<IEnumerable<ProductDto>>> GetProductsByConditionAsync(ProductFilterDto filter)
    {
        var expression = CreateExpression(filter);
        var result = await _productsRepository.GetProductsByConditionAsync(expression);
        var mapperResult = _mapper.Map<IEnumerable<ProductDto>>(result);
        return ProductResponse<IEnumerable<ProductDto>>.Success(mapperResult);
    }

    public async Task<ProductResponse<ProductDto>> GetProductByConditionAsync(ProductFilterDto filter)
    {
        var expression = CreateExpression(filter);
        var result = await _productsRepository.GetProductByConditionAsync(expression);
        return result is not null
            ? ProductResponse<ProductDto>.Success(_mapper.Map<ProductDto>(result))
            : ProductResponse<ProductDto>.Failure("Product not found");
    }

    public async Task<ProductResponse<ProductDto>> AddProductAsync(AddProductRequest addProductRequest)
    {
        var validationResult = await _addProductRequestValidator.ValidateAsync(addProductRequest);
        if (!validationResult.IsValid)
            return ProductResponse<ProductDto>.Failure("Validation failed",
                validationResult.Errors.Select(x => x.ErrorMessage));

        var product = _mapper.Map<Product>(addProductRequest);
        var result = await _productsRepository.AddProductAsync(product);

        if (result is null) return ProductResponse<ProductDto>.Failure("Can't create new product");

        return ProductResponse<ProductDto>.Success(_mapper.Map<ProductDto>(result));
    }

    public async Task<ProductResponse<ProductDto>> UpdateProductAsync(UpdateProductRequest updateProductRequest)
    {
        var validationResult = await _updateProductRequestValidator.ValidateAsync(updateProductRequest);
        if (!validationResult.IsValid)
            return ProductResponse<ProductDto>.Failure("Validation failed",
                validationResult.Errors.Select(x => x.ErrorMessage));

        var product = _mapper.Map<Product>(updateProductRequest);
        var result = await _productsRepository.UpdateProductAsync(product);

        return result is null
            ? ProductResponse<ProductDto>.Failure("Can't update product")
            : ProductResponse<ProductDto>.Success(_mapper.Map<ProductDto>(result));
    }

    public async Task<ProductResponse<ProductDto>> DeleteProductAsync(Guid id)
    {
        var product = await _productsRepository.GetProductByConditionAsync(x => x.Id == id);
        if (product is null) return ProductResponse<ProductDto>.Failure("Product not found");

        var isDeleted = await _productsRepository.DeleteProductAsync(product.Id);

        return isDeleted
            ? ProductResponse<ProductDto>.Success(_mapper.Map<ProductDto>(product), "Product deleted successfully")
            : ProductResponse<ProductDto>.Failure("Failed to delete product from database");
    }

    public async Task<ProductResponse<IEnumerable<ProductDto>>> UpdateProductStockAsync(
        ReduceStockRequest reduceStockRequest)
    {
        int maxRetries = 3;

        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                var result = await _productsRepository.GetProductsForUpdateAsync(reduceStockRequest.ReduceStockItems.Select(x => x.Id));

                if (reduceStockRequest.ReduceStockItems.Count() != result.Count()) return ProductResponse<IEnumerable<ProductDto>>.Failure("Some Products not found");

                if (reduceStockRequest.Reduce)
                {
                    foreach (var product in reduceStockRequest.ReduceStockItems)
                    {
                        var productToUpdate = result.First(x => x.Id == product.Id);
                        if (productToUpdate.QuantityInStock < product.Quantity)
                            return ProductResponse<IEnumerable<ProductDto>>.Failure($"Not enough product quantity in the stock for {product.Id}");   
                        
                        productToUpdate.QuantityInStock -= product.Quantity;
                    }

                }
                else
                {
                    // Kinda rollback if something happened after reducing 
                    result.QuantityInStock += reduceStockRequest.Quantity;
                }

                await _productsRepository.SaveProductsChangesAsync();

                return ProductResponse<IEnumerable<ProductDto>>.Success(_mapper.Map<IEnumerable<ProductDto>>(result));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (i == maxRetries - 1)
                    return ProductResponse<IEnumerable<ProductDto>>.Failure(
                        "Failed to reduce stock due to high traffic. Please try again.");

                int delay = Random.Shared.Next(10, 30) + (10 * i);
                await Task.Delay(delay);
            }
        }

        return ProductResponse<IEnumerable<ProductDto>>.Failure("Can't reduce the products stock");
    }

    private Expression<Func<Product, bool>> CreateExpression(ProductFilterDto filter)
    {
        Expression<Func<Product, bool>> expression = p =>
            (string.IsNullOrEmpty(filter.Name) || p.Name.Contains(filter.Name)) &&
            (string.IsNullOrEmpty(filter.Category) || p.Category == filter.Category) &&
            (!filter.MinPrice.HasValue || p.UnitPrice >= filter.MinPrice) &&
            (!filter.MaxPrice.HasValue || p.UnitPrice <= filter.MaxPrice) &&
            (!filter.MinQuantity.HasValue || p.QuantityInStock >= filter.MinQuantity) &&
            (!filter.MaxQuantity.HasValue || p.QuantityInStock <= filter.MaxQuantity);

        return expression;
    }
}