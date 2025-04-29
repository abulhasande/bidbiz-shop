using Discoun.Grpc.Models;
using Discount.Grpc;
using Discount.Grpc.Data;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Discoun.Grpc.Services
{
    public class DiscountService (DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            //get discount from DB
            var coupon = await dbContext.Coupones.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                coupon = new Coupon { ProductName = "No Disount", Amount = 0, Description = "No Disount" };

            logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel; 
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "InValid request object"));

            var sdfs = await dbContext.Coupones.FirstOrDefaultAsync(x => x.ProductName == request.Coupon.ProductName);

            dbContext.Coupones.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is succefully created. ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "InValid request object"));

            dbContext.Coupones.Update(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is succefully updated. ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async  Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {

            var coupon = await dbContext.Coupones.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product Name: {request.ProductName} not found"));

            dbContext.Coupones.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is succefully deleted. ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

            return new DeleteDiscountResponse { Success = true };

        }
    }
}
