using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.DBContext
{
    public class DiscountDBContext : DbContext
    {

        public DbSet<Coupon> Coupons { get; set; } 
        public DiscountDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(new List<Coupon>() {
                 new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                 new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
            });
            base.OnModelCreating(modelBuilder);
        }


    }
}
