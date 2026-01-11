namespace eCommerce.ProductService.BLL.DTOs;

public record ReduceStockRequest(int Quantity, bool Reduce = true);