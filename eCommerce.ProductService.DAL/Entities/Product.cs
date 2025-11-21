namespace eCommerce.ProductService.DAL.Entities;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Category { get; set; } = "Other";
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}