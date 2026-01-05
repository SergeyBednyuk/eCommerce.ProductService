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
        var builder = new MySqlConnectionStringBuilder
        {
            Server = configuration["MYSQL_HOST"] ?? throw new ArgumentNullException("MYSQL_HOST is missing in configuration"),
            Database = configuration["MYSQL_DATABASE"] ?? throw new ArgumentNullException("MYSQL_DATABASE is missing in configuration"),
            UserID = configuration["MYSQL_USER"] ?? throw new ArgumentNullException("MYSQL_USER is missing in configuration"),
            Password = configuration["MYSQL_PASSWORD"] ?? throw new ArgumentNullException("MYSQL_PASSWORD is missing in configuration"),
            Port = uint.TryParse(configuration["MYSQL_PORT"], out var port) ? port : 3306,
            AllowPublicKeyRetrieval = true,
            SslMode = MySqlSslMode.Disabled
        };

        services.AddDbContext<ApplicationDbContext>(options => { options.UseMySQL(builder.ConnectionString); });

        services.AddScoped<IProductsRepository, ProductRepository>();

        return services;
    }
}