using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Application.Services;

public interface IPromotionService
{
    Promotion Create(string name, string description, string code, decimal discountAmount, string discountCurrency, decimal discountPercentage,
        decimal minimumOrderAmount, string minimumOrderCurrency, decimal maximumDiscountAmount, string maximumDiscountCurrency, DateTime startDate, DateTime endDate, int usageLimit);
    bool IsValid(Promotion promotion);
    decimal CalculateDiscount(Promotion promotion, decimal orderAmount);
    bool CanBeUsedBy(Promotion promotion, string userId);
    void Use(Promotion promotion);
    void Deactivate(Promotion promotion);
    void AddApplicableUser(Promotion promotion, string userId);
    void RemoveApplicableUser(Promotion promotion, string userId);
}
