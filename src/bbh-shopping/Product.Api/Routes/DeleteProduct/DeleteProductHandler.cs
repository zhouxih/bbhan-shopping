using FluentValidation;
using Product.Api.Routes.CreateProduct;
using Product.Api.Services;
using SharedLib.CQRS;

namespace Product.Api.Routes.DeleteProduct
{
    public record DeleteProductCommand(Guid id):ICommand<bool>;

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.id).NotNull().NotEmpty().WithMessage("商品Id不能为空!");
        }
    }

    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
    {
        private readonly IProductService productService;
        public DeleteProductCommandHandler(IProductService productService) 
        {
            this.productService = productService;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var r = await productService.DeleteProductById(request.id);
            return r == 1;
        }
    }
}
