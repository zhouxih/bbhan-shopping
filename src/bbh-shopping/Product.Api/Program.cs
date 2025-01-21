using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Product.Api.Routes.CreateProduct;
using Product.Api.Services;
using SharedLib.CQRS.CQRS_AOP;
using SharedLib.ExceptionHandler;

namespace Product.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 添加 Swagger 服务
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IProductService,ProductService>();

            var thisAssembly = typeof(Program).Assembly;
            builder.Services.AddValidatorsFromAssembly(thisAssembly);
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            //注册MediatR服务
            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(thisAssembly);
                //处理参数验证 
                cfg.AddOpenBehavior(typeof(ValidateAop<,>));
                //记录日志
                cfg.AddOpenBehavior(typeof(LogAop<,>));

            });

            //添加Carter服务 (自定义路由)
            builder.Services.AddCarter();

            //注册martin服务
            builder.Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("ProductDB")!);
            }).UseLightweightSessions();

            //添加健康检查

            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("ProductDB")!);



            var app = builder.Build();

            // 使用 Swagger 中间件
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health",
             new HealthCheckOptions
             {
                 ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
             });
            // 使用 Carter 路由
            app.MapCarter();
         

            app.Run();
        }
    }
}
