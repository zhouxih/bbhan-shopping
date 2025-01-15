using Carter;

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

            //添加Carter服务 (自定义路由)
            builder.Services.AddCarter();

            var app = builder.Build();

            // 使用 Swagger 中间件
            app.UseSwagger();
            app.UseSwaggerUI();

            // 使用 Carter 路由
            app.MapCarter();

            app.MapGet("/", () => "Hello World!");


            app.Run();
        }
    }
}
