using eCommerce.ProductService.DAL.Context;
using eCommerce.ProductService.DAL.Repositories;
using eCommerce.ProductService.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.ProductService.DAL.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!);
        });
        
        services.AddScoped<IProductsRepository, ProductRepository>();
        
        return services;
    }
}