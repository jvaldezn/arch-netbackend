using API;
using API.Extensions;
using Data.Dto;
using Domain.UseCase;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace API.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Product").WithTags(nameof(ProductDto));

        group.MapGet("/", async (IProductUseCase productUseCase) =>
        {
            return Results.Ok(await productUseCase.GetAllProducts());
        })
        .WithName("GetAllProducts")
        .WithOpenApi();

        group.MapGet("/{id}", async (int id, IProductUseCase productUseCase) =>
        {
            return Results.Ok(await productUseCase.GetProductById(id));
        })
        .WithName("GetProductById")
        .WithOpenApi();

        group.MapPut("/{id}", async (int id, ProductDto productDto, IProductUseCase productUseCase) =>
        {
            return Results.Ok(await productUseCase.UpdateProduct(id, productDto));
        })
        .WithName("UpdateProduct")
        .WithOpenApi();

        group.MapPost("/", async (ProductDto productDto, IProductUseCase productUseCase) =>
        {
            return Results.Ok(await productUseCase.CreateProduct(productDto));
        })
        .WithName("CreateProduct")
        .WithValidation<ProductDto>()
        .WithOpenApi();

        group.MapDelete("/{id}", async (int id, IProductUseCase productUseCasee) =>
        {
            return Results.Ok(await productUseCasee.DeleteProduct(id));
        })
        .WithName("DeleteProduct")
        .WithOpenApi();
    }
}
