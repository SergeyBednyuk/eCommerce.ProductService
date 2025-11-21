namespace eCommerce.ProductService.BLL.DTOs;

public record AddProductRequest(string Name, CategoryOptions Category, decimal UnitPrice, int QuantityInStock);