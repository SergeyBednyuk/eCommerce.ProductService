using System.Linq.Expressions;
using AutoMapper;
using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.BLL.ServicesInterfaces;
using eCommerce.ProductService.DAL.RepositoryInterfaces;
using FluentValidation;

namespace eCommerce.ProductService.BLL.Services;

public class ProductsService(IProductsRepository repository, IMapper mapper) : IProductsService
{
    private readonly IProductsRepository  _repository = repository;
    private readonly IMapper _mapper = mapper;
    //Validators
    private readonly IValidator<AddProductRequest> _addProductRequestValidator;
    private readonly IValidator<UpdateProductRequest> _updateProductRequestValidator;


    public Task<ProductResponse<ProductDto>> GetProductsAsync(int page = 1, int pageSize = 10)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse<ProductDto>> GetProductsByConditionAsync(Expression<Func<ProductResponse<ProductDto>, bool>> conditionExpression)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse<ProductDto>> GetProductByConditionAsync(Expression<Func<ProductResponse<ProductDto>, bool>> conditionExpression)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse<ProductDto>> AddProductAsync(AddProductRequest addProductRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse<ProductDto>> UpdateProductAsync(UpdateProductRequest updateProductRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse<ProductDto>> DeleteProductAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}