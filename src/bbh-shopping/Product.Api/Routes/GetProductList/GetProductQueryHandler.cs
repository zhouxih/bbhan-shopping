using Mapster;
using Product.Api.Models;
using Product.Api.Services;
using SharedLib.CQRS;

namespace Product.Api.Routes.GetProductList
{
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<ShopProduct> ShopProducts);

    public class GetProductQueryHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
    {

        private readonly IProductService productService;
        public GetProductQueryHandler(IProductService productService) 
        {
            this.productService = productService;
        }

        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var query = request.Adapt<GetProductsQuery>();
            var productList = await productService.ProductList(query.PageNumber, query.PageSize);
            return new GetProductsResult(productList);
        }
    }
}
