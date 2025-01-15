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

            var app = builder.Build();

            // 使用 Swagger 中间件
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapGet("/", () => "Hello World!");


            app.Run();
        }
    }
}
