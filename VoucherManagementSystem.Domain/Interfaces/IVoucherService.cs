using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Domain.Interfaces;

public interface IVoucherService
{
    Voucher Create(string userId, string promotionId, string code, decimal discountAmount, string discountCurrency, decimal orderAmount, string orderCurrency, decimal finalDiscountAmount, string finalDiscountCurrency);
    void Use(Voucher voucher, string orderId);
    bool CanBeUsed(Voucher voucher);
}
