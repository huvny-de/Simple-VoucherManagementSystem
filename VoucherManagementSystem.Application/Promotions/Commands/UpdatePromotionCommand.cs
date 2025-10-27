using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;

namespace VoucherManagementSystem.Application.Promotions.Commands;

public record UpdatePromotionCommand(
    string Id,
    string Name,
    string Description,
    decimal DiscountAmount,
    string DiscountCurrency,
    decimal DiscountPercentage,
    decimal MinimumOrderAmount,
    string MinimumOrderCurrency,
    decimal MaximumDiscountAmount,
    string MaximumDiscountCurrency,
    DateTime StartDate,
    DateTime EndDate,
    int UsageLimit
) : ICommand<ApplicationOperationResult<PromotionDto>>;
