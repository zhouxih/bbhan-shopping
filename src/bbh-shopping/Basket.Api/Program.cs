using Carter;
using Marten;

namespace Basket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //���Carter���� (�Զ���·��)
            builder.Services.AddCarter();
            //ע��martin����
            builder.Services.AddMarten(options =>
            {
                options.Connection(builder.Configuration.GetConnectionString("ProductDB")!);
            }).UseLightweightSessions();

            //��ӽ������

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
