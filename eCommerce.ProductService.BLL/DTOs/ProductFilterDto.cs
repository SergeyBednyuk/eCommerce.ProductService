namespace eCommerce.ProductService.BLL.DTOs;

public record ProductFilterDto(string? Name, string? Category, decimal? MinPrice, decimal? MaxPrice, int? MinQuantity, int? MaxQuantity);