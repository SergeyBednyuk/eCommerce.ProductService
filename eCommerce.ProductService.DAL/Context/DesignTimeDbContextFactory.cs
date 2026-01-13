using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eCommerce.ProductService.DAL.Context;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var connectionString = $"Server=localhost;Port=3306;Database=ecommerce_productsdatabase;User=root;Password=Legion13$%;";
        
        optionsBuilder.UseMySQL(connectionString);
        
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}