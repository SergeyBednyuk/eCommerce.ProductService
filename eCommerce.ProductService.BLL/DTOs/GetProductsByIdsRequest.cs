namespace eCommerce.ProductService.BLL.DTOs;

public record GetProductsByIdsRequest(IEnumerable<Guid> Ids) { }