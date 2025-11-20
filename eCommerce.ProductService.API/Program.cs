using eCommerce.ProductService.API.Middlewares;
using eCommerce.ProductService.BLL.Extensions;
using eCommerce.ProductService.DAL.Extensions;

namespace eCommerce.ProductService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddBusinessLogic();
        builder.Services.AddDataAccessLayer();
        
        builder.Services.AddControllers();
        
        var app = builder.Build();

        app.UseGlobalExceptionHandlerMiddleware();

        app.UseRouting();
        
        //Auth
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        
        app.Run();
    }
}