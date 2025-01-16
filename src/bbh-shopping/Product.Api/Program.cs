using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Product.Api.Routes.CreateProduct;
using SharedLib.CQRS.CQRS_AOP;

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

            var thisAssembly = typeof(Program).Assembly;
            builder.Services.AddValidatorsFromAssembly(thisAssembly);


            //ע��MediatR����
            builder.Services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(thisAssembly);
                //��¼��־
                cfg.AddOpenBehavior(typeof(ValidateAOP<,>));
                //���������֤ 
            });

            //���Carter���� (�Զ���·��)
            builder.Services.AddCarter();




            var app = builder.Build();

            // ʹ�� Swagger �м��
            app.UseSwagger();
            app.UseSwaggerUI();

            // ʹ�� Carter ·��
            app.MapCarter();

            app.MapGet("/", () => "Hello World!");


            app.Run();
        }
    }
}
