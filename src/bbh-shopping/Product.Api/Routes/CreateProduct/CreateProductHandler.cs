using FluentValidation;
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
        public CreateProductHandler() 
        {

        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //保存数据库
            Guid id = Guid.NewGuid();

            //返回值
            return new CreateProductCommandResponse(id);
        }
    }
}
