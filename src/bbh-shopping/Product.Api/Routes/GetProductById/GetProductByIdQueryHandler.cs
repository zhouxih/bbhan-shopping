using Product.Api.Models;
using Product.Api.Services;
using SharedLib.CQRS;

namespace Product.Api.Routes.GetProductById
{
    public record GetProductByIdQuery(Guid id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(ShopProduct ShopProduct);
    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IProductService productService;
        public GetProductByIdQueryHandler(IProductService productService) 
        {
            this.productService = productService;
        }
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var shopProduct = await this.productService.GetProductById(request.id);
            return new GetProductByIdResult(shopProduct);
        }
    }
}
