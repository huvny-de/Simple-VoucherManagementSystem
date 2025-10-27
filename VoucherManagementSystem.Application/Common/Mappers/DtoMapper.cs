using VoucherManagementSystem.Application.Promotions.DTOs;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;
using VoucherManagementSystem.Application.Users.DTOs;
using VoucherManagementSystem.Application.Vouchers.DTOs;
using VoucherManagementSystem.Domain.Entities;

namespace VoucherManagementSystem.Application.Common.Mappers;

public static class DtoMapper
{
    // User mappers
    public static UserDto ToDto(this User user, IUserService userService)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = userService.GetFullName(user),
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth,
            Address = user.Address,
            IsActive = user.IsActive,
            LastLoginAt = user.LastLoginAt,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public static IEnumerable<UserDto> ToDto(this IEnumerable<User> users, IUserService userService)
    {
        return users.Select(u => u.ToDto(userService));
    }

    // Promotion mappers
    public static PromotionDto ToDto(this Promotion promotion, IPromotionService promotionService)
    {
        return new PromotionDto
        {
            Id = promotion.Id,
            Name = promotion.Name,
            Description = promotion.Description,
            Code = promotion.Code,
            DiscountAmount = promotion.DiscountAmount,
            DiscountCurrency = promotion.DiscountCurrency,
            DiscountPercentage = promotion.DiscountPercentage,
            MinimumOrderAmount = promotion.MinimumOrderAmount,
            MinimumOrderCurrency = promotion.MinimumOrderCurrency,
            MaximumDiscountAmount = promotion.MaximumDiscountAmount,
            MaximumDiscountCurrency = promotion.MaximumDiscountCurrency,
            StartDate = promotion.StartDate,
            EndDate = promotion.EndDate,
            UsageLimit = promotion.UsageLimit,
            UsedCount = promotion.UsedCount,
            IsActive = promotion.IsActive,
            IsValid = promotionService.IsValid(promotion),
            ApplicableUserIds = promotion.ApplicableUserIds,
            CreatedAt = promotion.CreatedAt,
            UpdatedAt = promotion.UpdatedAt
        };
    }

    public static IEnumerable<PromotionDto> ToDto(this IEnumerable<Promotion> promotions, IPromotionService promotionService)
    {
        return promotions.Select(p => p.ToDto(promotionService));
    }

    // Voucher mappers
    public static VoucherDto ToDto(this Voucher voucher)
    {
        return new VoucherDto
        {
            Id = voucher.Id,
            UserId = voucher.UserId,
            PromotionId = voucher.PromotionId,
            Code = voucher.Code,
            DiscountAmount = voucher.DiscountAmount,
            DiscountCurrency = voucher.DiscountCurrency,
            OrderAmount = voucher.OrderAmount,
            OrderCurrency = voucher.OrderCurrency,
            FinalDiscountAmount = voucher.FinalDiscountAmount,
            FinalDiscountCurrency = voucher.FinalDiscountCurrency,
            UsedAt = voucher.UsedAt,
            IsUsed = voucher.IsUsed,
            OrderId = voucher.OrderId,
            CreatedAt = voucher.CreatedAt,
            UpdatedAt = voucher.UpdatedAt
        };
    }

    public static IEnumerable<VoucherDto> ToDto(this IEnumerable<Voucher> vouchers)
    {
        return vouchers.Select(v => v.ToDto());
    }
}
