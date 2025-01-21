using Carter;
using Mapster;
using MediatR;
using Product.Api.Models;

namespace Product.Api.Routes.GetProductById
{

    public record GetProductByIdResponse(ShopProduct ShopProduct);
    public class GetProductByIdRoute : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Product/{id}", async(Guid id, ISender sender) => {
                var result = await sender.Send(new GetProductByIdQuery(id));
                return  result.Adapt<GetProductByIdResponse>();

            }).WithName("GetProduct")
              .WithSummary("获取商品 By Id")
              .WithDescription("获取商品 By Id");
        }
    }
}
