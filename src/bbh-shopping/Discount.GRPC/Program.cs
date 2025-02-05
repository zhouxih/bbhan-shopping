using Discount.GRPC.DBContext;
using Discount.GRPC.Services;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var dbString = builder.Configuration.GetConnectionString("DataBase");
            builder.Services.AddGrpc();
            builder.Services.AddDbContext<DiscountDBContext>(o=>o.UseSqlite("dbString"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<CouponService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}