using System.Text.Json.Serialization;
using eCommerce.ProductService.API.APIEndpoints;
using eCommerce.ProductService.API.Infrastructure;
using eCommerce.ProductService.API.Middlewares;
using eCommerce.ProductService.BLL.Extensions;
using eCommerce.ProductService.DAL.Extensions;

namespace eCommerce.ProductService.API;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddBusinessLogic();
            builder.Services.AddDataAccessLayer(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            //Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                });
            });

            //New approach for Global exceptions handling
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            //Old approach for Global exceptions handling
            // app.UseGlobalExceptionHandlerMiddleware();
            //New approach for Global exceptions handling
            app.UseExceptionHandler();

            app.UseRouting();

            //CORS
            app.UseCors();

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            //Auth
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapProductApiEndpoints();

            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CRITICAL STARTUP ERROR: {ex.Message}");
            throw;
        }
    }
}