using Carter;
using Mapster;
using MediatR;

namespace Product.Api.Routes.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record CreateProductResponse(Guid Id);


    public class CreateProductRoute : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // 自定义 GET 路由
            app.MapPost("/AddProduct", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                return result;

            }).WithName("新增商品 Name")
              .WithSummary("新增商品 Summary")
              .WithDescription("新增商品 Description")
              .Produces<CreateProductResponse>(StatusCodes.Status201Created)
              .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
