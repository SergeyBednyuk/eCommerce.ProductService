using eCommerce.ProductService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.ProductService.DAL.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnType("char(36)");
        
        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(p => p.Category)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.UnitPrice)
            .HasPrecision(10, 2);
    }
}