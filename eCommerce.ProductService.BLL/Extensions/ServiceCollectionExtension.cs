using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.BLL.Mappers;
using eCommerce.ProductService.BLL.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductService.BLL.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(ProductMappingProfile));
        
        services.AddScoped<IValidator<AddProductRequest>, AddProductRequestValidator>();
        services.AddScoped<IValidator<UpdateProductRequest>, UpdateProductRequestValidator>();
        
        return services;
    }
}