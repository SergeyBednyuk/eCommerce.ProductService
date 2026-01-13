namespace eCommerce.ProductService.BLL.DTOs;

public record UpdateStockRequest(IEnumerable<UpdateStockDto> UpdateStockItems, bool Reduce = true);