using FluentValidation;
using Mapster;
using Product.Api.Models;
using Product.Api.Routes.DeleteProduct;
using Product.Api.Services;
using SharedLib.CQRS;

namespace Product.Api.Routes.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price):ICommand<UpdateProductResponse>;
    public record UpdateProductResponse(ShopProduct ShopProduct);


    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("商品Id不能为空!");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("商品名称不能为空!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("商品价格必须大于0!");
            RuleFor(x => x.Category).NotNull().Must(x => x.Count > 0).WithMessage($"必须要有类别!");
        }
    }
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly IProductService productService;
        public UpdateProductCommandHandler(IProductService productService) 
        {
            this.productService = productService;
        }
        public async  Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var sp = request.Adapt<ShopProduct>();
            var result = await this.productService.UpdateProduct(sp);
            return new UpdateProductResponse(result);
        }
    }
}
