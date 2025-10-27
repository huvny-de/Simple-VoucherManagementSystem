using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Application.Services;

public class VoucherService : IVoucherService
{
    public Voucher Create(string userId, string promotionId, string code, decimal discountAmount, string discountCurrency, decimal orderAmount, string orderCurrency, decimal finalDiscountAmount, string finalDiscountCurrency)
    {
        return new Voucher
        {
            UserId = userId,
            PromotionId = promotionId,
            Code = code,
            DiscountAmount = discountAmount,
            DiscountCurrency = discountCurrency,
            OrderAmount = orderAmount,
            OrderCurrency = orderCurrency,
            FinalDiscountAmount = finalDiscountAmount,
            FinalDiscountCurrency = finalDiscountCurrency,
            OrderId = string.Empty,
            IsUsed = false
        };
    }

    public void Use(Voucher voucher, string orderId)
    {
        if (voucher.IsUsed)
            throw new InvalidOperationException("Voucher has already been used");

        voucher.IsUsed = true;
        voucher.UsedAt = DateTime.UtcNow;
        voucher.OrderId = orderId;
        voucher.UpdatedAt = DateTime.UtcNow;
    }

    public bool CanBeUsed(Voucher voucher)
    {
        return !voucher.IsUsed;
    }
}
