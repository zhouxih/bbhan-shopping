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

            var app = builder.Build();

            // ʹ�� Swagger �м��
            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapGet("/", () => "Hello World!");


            app.Run();
        }
    }
}
