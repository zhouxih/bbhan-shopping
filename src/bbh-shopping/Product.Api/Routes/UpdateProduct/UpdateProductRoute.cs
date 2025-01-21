using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Models;

namespace Product.Api.Routes.UpdateProduct
{
    public record UpdateProductRequest(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResult(ShopProduct ShopProduct);
    public class UpdateProductRoute : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Product/Update", async ([FromBody] UpdateProductRequest updateProductRequest,ISender sender) => {
                var command = updateProductRequest.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                return result.Adapt<UpdateProductResult>();
            })
              .WithName("UpdateProduct")
              .WithSummary("获取商品 By Id")
              .WithDescription("获取商品 By Id");
        }
    }
}
