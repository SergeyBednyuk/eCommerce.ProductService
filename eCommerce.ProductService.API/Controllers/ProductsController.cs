using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.BLL.ServicesInterfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.ProductService.API.Controllers;

[ApiController]
[Route("api/Products")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductsService productsService, ILogger<ProductsController> logger,
        IValidator<AddProductRequest> addProductRequestValidator,
        IValidator<UpdateProductRequest> updateProductRequestValidator)
    {
        _productsService = productsService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductAsync(AddProductRequest addProductRequest)
    {
        _logger.LogInformation("adding new product");
        
        var response = await _productsService.AddProductAsync(addProductRequest);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        
        return Ok(response);
        
    }
}