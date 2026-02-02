using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.BLL.Mappers;
using eCommerce.ProductService.BLL.RabbitMQ;
using eCommerce.ProductService.BLL.Services;
using eCommerce.ProductService.BLL.ServicesInterfaces;
using eCommerce.ProductService.BLL.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace eCommerce.ProductService.BLL.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(cfg => { }, typeof(ProductMappingProfile));

        services.AddScoped<IValidator<AddProductRequest>, AddProductRequestValidator>();
        services.AddScoped<IValidator<UpdateProductRequest>, UpdateProductRequestValidator>();
        services.AddScoped<IValidator<GetProductsByIdsRequest>, GetProductsByIdsRequestValidator>();

        services.AddScoped<IProductsService, ProductsService>();

        services.AddSingleton<RabbitMqPublisher>();
        
        services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory()
        {
            HostName = configuration["RABBITMQ_HOST"] ?? "localhost",
            Port = Int32.TryParse(configuration["RABBITMQ_PORT"], out int p) && p != 0 ? p : 5672,
            UserName = configuration["RABBITMQ_DEFAULT_USER"] ?? "user",
            Password = configuration["RABBITMQ_DEFAULT_PASS"] ?? "password"
        });

        services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
        services.AddSingleton<IConnection>(sp =>
        {
            var factory = sp.GetRequiredService<IConnectionFactory>();
            var logger = sp.GetRequiredService<ILogger<RabbitMqPublisher>>();

            int retryCount = 0;
            while (retryCount < 5)
            {
                try
                {
                    return factory.CreateConnectionAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    retryCount++;
                    logger.LogWarning(ex, "RabbitMQ connection failed. Retrying {RetryCount}/5 in 2 seconds...",
                        retryCount);

                    if (retryCount >= 5)
                    {
                        throw;
                    }

                    Thread.Sleep(2000);
                }
            }

            throw new InvalidOperationException("Could not connect to RabbitMQ.");
        });
        
        

        return services;
    }
}