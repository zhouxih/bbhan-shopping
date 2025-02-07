using Carter;
using Marten;

namespace Basket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //添加Carter服务 (自定义路由)
            builder.Services.AddCarter();
            //注册martin服务
            builder.Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("ProductDB")!);
            }).UseLightweightSessions();

            //添加健康检查

            builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("ProductDB")!)
                .AddRedis(builder.Configuration.GetConnectionString("Redis")!);



            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.UseHealthChecks("/health");
            app.Run();
        }
    }
}
