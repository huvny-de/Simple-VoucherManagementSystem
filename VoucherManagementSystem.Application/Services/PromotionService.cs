using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Application.Services;

public class PromotionService : IPromotionService
{
    public Promotion Create(string name, string description, string code, decimal discountAmount, string discountCurrency, decimal discountPercentage,
        decimal minimumOrderAmount, string minimumOrderCurrency, decimal maximumDiscountAmount, string maximumDiscountCurrency, DateTime startDate, DateTime endDate, int usageLimit)
    {
        // Validation logic (can be moved to FluentValidation in Application layer)
        if (startDate >= endDate)
            throw new ArgumentException("Start date must be before end date");

        if (usageLimit <= 0)
            throw new ArgumentException("Usage limit must be greater than 0");

        return new Promotion
        {
            Name = name,
            Description = description,
            Code = code,
            DiscountAmount = discountAmount,
            DiscountCurrency = discountCurrency,
            DiscountPercentage = discountPercentage,
            MinimumOrderAmount = minimumOrderAmount,
            MinimumOrderCurrency = minimumOrderCurrency,
            MaximumDiscountAmount = maximumDiscountAmount,
            MaximumDiscountCurrency = maximumDiscountCurrency,
            StartDate = startDate,
            EndDate = endDate,
            UsageLimit = usageLimit,
            IsActive = true,
            UsedCount = 0,
            ApplicableUserIds = new List<string>()
        };
    }

    public bool IsValid(Promotion promotion)
    {
        return promotion.IsActive && DateTime.UtcNow >= promotion.StartDate && DateTime.UtcNow <= promotion.EndDate && promotion.UsedCount < promotion.UsageLimit;
    }

    public decimal CalculateDiscount(Promotion promotion, decimal orderAmount)
    {
        if (!IsValid(promotion))
            return 0;

        if (orderAmount < promotion.MinimumOrderAmount)
            return 0;

        var percentageDiscount = orderAmount * (promotion.DiscountPercentage / 100);
        var finalDiscount = promotion.DiscountAmount > 0 ? promotion.DiscountAmount : percentageDiscount;

        if (promotion.MaximumDiscountAmount > 0 && finalDiscount > promotion.MaximumDiscountAmount)
            finalDiscount = promotion.MaximumDiscountAmount;

        return finalDiscount;
    }

    public bool CanBeUsedBy(Promotion promotion, string userId)
    {
        if (!IsValid(promotion))
            return false;

        if (promotion.ApplicableUserIds.Count == 0)
            return true;

        return promotion.ApplicableUserIds.Contains(userId);
    }

    public void Use(Promotion promotion)
    {
        if (!IsValid(promotion))
            throw new InvalidOperationException("Promotion is not valid for use");

        promotion.UsedCount++;
        promotion.UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate(Promotion promotion)
    {
        promotion.IsActive = false;
        promotion.UpdatedAt = DateTime.UtcNow;
    }

    public void AddApplicableUser(Promotion promotion, string userId)
    {
        if (!promotion.ApplicableUserIds.Contains(userId))
        {
            promotion.ApplicableUserIds.Add(userId);
            promotion.UpdatedAt = DateTime.UtcNow;
        }
    }

    public void RemoveApplicableUser(Promotion promotion, string userId)
    {
        promotion.ApplicableUserIds.Remove(userId);
        promotion.UpdatedAt = DateTime.UtcNow;
    }
}
