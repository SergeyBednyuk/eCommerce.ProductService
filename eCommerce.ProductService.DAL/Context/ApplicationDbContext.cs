using eCommerce.ProductService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.ProductService.DAL.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product>  Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // 3. Organizing Configurations: 
        // Instead of cluttering this file with Fluent API code, 
        // this line automatically finds all IEntityTypeConfiguration classes.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}