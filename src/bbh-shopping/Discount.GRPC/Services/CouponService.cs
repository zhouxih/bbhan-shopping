using Discount.GRPC.DBContext;
using Discount.GRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services
{
    public class CouponService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly DiscountDBContext discountDBContext;
        public CouponService(DiscountDBContext discountDBContext) { 
            this.discountDBContext = discountDBContext;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            discountDBContext.Coupons.Update(coupon);
            await discountDBContext.SaveChangesAsync();
            var model = coupon.Adapt<CouponModel>();
            return model;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            discountDBContext.Coupons.Add(coupon);
            await discountDBContext.SaveChangesAsync();

            var model = coupon.Adapt<CouponModel>();
            return model;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var productName = request.ProductName;
            var coupon = await discountDBContext.Coupons.Where(c => c.ProductName == productName).FirstOrDefaultAsync();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            discountDBContext.Coupons.Remove(coupon);
            await discountDBContext.SaveChangesAsync();
            return new DeleteDiscountResponse() { Success = true } ;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await this.discountDBContext.Coupons
                .Where(c => c.ProductName == request.ProductName)
                .FirstOrDefaultAsync();
            if (coupon is null) 
            {
                coupon = new Models.Coupon { ProductName = "Not Found", Amount = 0, Description = "Not Found" };
                return coupon.Adapt<CouponModel>();
            }

            var model = coupon.Adapt<CouponModel>();
            return model;
        }
    }
}
