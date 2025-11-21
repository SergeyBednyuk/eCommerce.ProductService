namespace eCommerce.ProductService.BLL.DTOs;

public record UpdateProductRequest(Guid Id, string Name, CategoryOptions Category, decimal UnitPrice, int QuantityInStock);