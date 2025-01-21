using Carter;
using Mapster;
using MediatR;
using Product.Api.Models;

namespace Product.Api.Routes.GetProductList
{
    public class GetProductListRoute : ICarterModule
    {
        public record GetProductListRequest(int? PageIndex,int? PageSize);
        public record GetProductListResponse(IEnumerable<ShopProduct> ShopProducts);


        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // 自定义 GET 路由
            app.MapGet("/GetProductList", async ([AsParameters] GetProductListRequest request, ISender sender) => {
                var query = request.Adapt<GetProductsQuery>();
                var r = await sender.Send(query);
                var res = r.Adapt<GetProductListResponse>();
                return res;
            }).WithName("GetProductList")
              .WithSummary("获取商品列表")
              .WithDescription("获取商品列表 可传入页码等信息");

    
        }
    }
}
