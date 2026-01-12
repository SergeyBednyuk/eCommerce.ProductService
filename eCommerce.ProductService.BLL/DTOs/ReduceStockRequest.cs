namespace eCommerce.ProductService.BLL.DTOs;

public record ReduceStockRequest(IEnumerable<ReduceStockDto> ReduceStockItems, bool Reduce = true);