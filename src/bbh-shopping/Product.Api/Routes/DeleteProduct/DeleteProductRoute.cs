using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Models;

namespace Product.Api.Routes.DeleteProduct
{
    public record DeleteProductRequest(Guid id);

    public record DeleteProductResult(bool Success);

    public class DeleteProductRoute : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Product/Delete", async ([FromBody] DeleteProductRequest request,ISender sender) => {

                var result = await sender.Send(new DeleteProductCommand(request.id));
                return new DeleteProductResult(result);

            }).WithName("DeleteProduct")
              .WithSummary("获取商品 By Id")
              .WithDescription("获取商品 By Id");

        }
    }
}
