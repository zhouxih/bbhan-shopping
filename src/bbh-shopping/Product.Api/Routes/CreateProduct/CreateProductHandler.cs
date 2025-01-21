using FluentValidation;
using Mapster;
using Product.Api.Services;
using SharedLib.CQRS;


namespace Product.Api.Routes.CreateProduct
{

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("商品名称不能为空!");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("商品价格必须大于0!");
            RuleFor(x => x.Category).NotNull().Must(x => x.Count > 0).WithMessage($"必须要有类别!");
        }
    }

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductCommandResponse>;

    public record CreateProductCommandResponse(Guid Id);


    public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResponse>
    {
        private readonly IProductService productService;
        public CreateProductHandler(IProductService productService) 
        {
            this.productService = productService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //保存数据库
            var product = request.Adapt<Product.Api.Models.ShopProduct>();
            product.Id = Guid.NewGuid();
            await this.productService.AddProduct(product);

            //返回值
            return new CreateProductCommandResponse(product.Id);
        }
    }
}
