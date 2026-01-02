using eCommerce.ProductService.DAL.Context;
using eCommerce.ProductService.DAL.Repositories;
using eCommerce.ProductService.DAL.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace eCommerce.ProductService.DAL.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // var connectionStringTemplate = configuration.GetConnectionString("DefaultConnection")!;
        // var connectionString = connectionStringTemplate
        //     .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST") ?? "localhost")
        //     .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "Legiopn13$%")
        //     .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306")
        //     .Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "ecommerce_productsdatabase")
        //     .Replace("$MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root");

        var builder = new MySqlConnectionStringBuilder();
        builder.Server = configuration["MYSQL_HOST"] ?? "localhost";
        builder.Database = configuration["MYSQL_DATABASE"] ?? "ecommerce_productsdatabase";
        builder.UserID = configuration["MYSQL_USER"] ?? "root";
        builder.Password = configuration["MYSQL_PASSWORD"];
        builder.Port = uint.TryParse(configuration["MYSQL_PORT"], out var port) ? port : 3306;

        services.AddDbContext<ApplicationDbContext>(options => { options.UseMySQL(builder.ConnectionString); });

        services.AddScoped<IProductsRepository, ProductRepository>();

        return services;
    }
}