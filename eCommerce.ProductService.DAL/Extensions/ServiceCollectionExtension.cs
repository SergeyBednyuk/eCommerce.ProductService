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
        var builder = new MySqlConnectionStringBuilder();
        builder.Server = configuration["MYSQL_HOST"] ?? "localhost";
        builder.Database = configuration["MYSQL_DATABASE"] ?? "ecommerce_productsdatabase";
        builder.UserID = configuration["MYSQL_USER"] ?? "root";
        builder.Password = configuration["MYSQL_PASSWORD"];
        builder.Port = uint.TryParse(configuration["MYSQL_PORT"], out var port) ? port : 3306;

        builder.AllowPublicKeyRetrieval = true;
        builder.SslMode = MySqlSslMode.Disabled;

        services.AddDbContext<ApplicationDbContext>(options => { options.UseMySQL(builder.ConnectionString); });

        services.AddScoped<IProductsRepository, ProductRepository>();

        return services;
    }
}