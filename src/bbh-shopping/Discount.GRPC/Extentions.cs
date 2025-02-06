using Discount.GRPC.DBContext;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC
{
    public static class Extentions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDBContext>();
            dbContext.Database.MigrateAsync();

            return app;
        }
    }
}
