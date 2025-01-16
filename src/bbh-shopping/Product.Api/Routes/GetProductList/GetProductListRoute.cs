using Carter;

namespace Product.Api.Routes.GetProductList
{
    public class GetProductListRoute : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // 自定义 GET 路由
            app.MapGet("/GetProductList", () => "This is a custom GET route")
               .WithName("GetProductList")
               .WithSummary("获取商品列表")
               .WithDescription("获取商品列表 可传入页码等信息");

    
        }
    }
}
