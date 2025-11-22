namespace eCommerce.ProductService.BLL.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Category { get; set; }
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
}