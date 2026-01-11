using eCommerce.ProductService.BLL.DTOs;
using eCommerce.ProductService.BLL.ServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.ProductService.API.APIEndpoints;

public static class ProductApiEndpoints
{
    public static IEndpointRouteBuilder MapProductApiEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/products").WithTags("Products");


        //Get /api/products/all
        group.MapGet("all", async (IProductsService productsService) =>
        {
            var response = await productsService.GetProductsAsync();
            return Results.Ok(response);
        });

        //Get /api/products?page=1&pageSize=10
        group.MapGet("/",
            async (IProductsService productsService, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 30) =>
            {
                var response = await productsService.GetProductsAsync(pageNumber, pageSize);
                return Results.Ok(response);
            });

        //Get /api/products/search/{id}
        group.MapGet("/{productId:guid}", async (IProductsService productsService, Guid productId) =>
        {
            var response = await productsService.GetProductAsync(productId);
            return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
        });
        
        //Get /api/products/search/batch
        group.MapPost("/search/batch", async (IProductsService productsService, [FromBody] GetProductsByIdsRequest ids) =>
        {
            var response = await productsService.GetProductsByIdsAsync(ids);
            return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
        });
        
        //Get /api/products/search
        group.MapGet("/search", async (IProductsService productsService, [AsParameters] ProductFilterDto filter) =>
        {
            var response = await productsService.GetProductByConditionAsync(filter);
            return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
        });
        
        //Get /api/products/search/all
        group.MapGet("/search/all", async (IProductsService productsService, [AsParameters] ProductFilterDto filter) =>
        {
            var response = await productsService.GetProductsByConditionAsync(filter);
            return Results.Ok(response);
        });

        //Post /api/products
        group.MapPost("/", async (IProductsService productsService, [FromBody] AddProductRequest newProduct) =>
        {
            var response = await productsService.AddProductAsync(newProduct);
            return response.IsSuccess
                ? Results.Created($"/api/products/{response.Data!.Id}", response)
                : Results.BadRequest(response);
        });
        
        //Put /api/products/{id}/reduce-stock
        group.MapPut("/{productId:guid}/update-stock", async (IProductsService productsService, Guid productId, [FromBody] ReduceStockRequest request) =>
        {
            var response = await productsService.UpdateProductStockAsync(productId, request);
            return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
        });
        
        //Put /api/products
        group.MapPut("/", async (IProductsService productsService, [FromBody] UpdateProductRequest updatedProduct) =>
        {
            var response = await productsService.UpdateProductAsync(updatedProduct);
            return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
        });

        //Delete /api/products/{id}
        group.MapDelete("/{productId:guid}", async (IProductsService productsService, Guid productId) =>
        {
            var response = await productsService.DeleteProductAsync(productId);
            return response.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
        });
        return builder;
    }
}