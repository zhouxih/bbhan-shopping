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

            // ��� Swagger ����
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IProductService,ProductService>();

            var thisAssembly = typeof(Program).Assembly;
            builder.Services.AddValidatorsFromAssembly(thisAssembly);
            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            //ע��MediatR����
            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(thisAssembly);
                //���������֤ 
                cfg.AddOpenBehavior(typeof(ValidateAop<,>));
                //��¼��־
                cfg.AddOpenBehavior(typeof(LogAop<,>));

            });

            //���Carter���� (�Զ���·��)
            builder.Services.AddCarter();

            //ע��martin����
            builder.Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("ProductDB")!);
            }).UseLightweightSessions();

            //��ӽ������

            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("ProductDB")!);



            var app = builder.Build();

            // ʹ�� Swagger �м��
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health",
             new HealthCheckOptions
             {
                 ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
             });
            // ʹ�� Carter ·��
            app.MapCarter();
         

            app.Run();
        }
    }
}
