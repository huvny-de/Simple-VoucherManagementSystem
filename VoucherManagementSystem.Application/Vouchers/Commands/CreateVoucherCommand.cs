using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Vouchers.DTOs;

namespace VoucherManagementSystem.Application.Vouchers.Commands;

public record CreateVoucherCommand(
    string UserId,
    string PromotionId,
    string Code,
    decimal DiscountAmount,
    string DiscountCurrency,
    decimal OrderAmount,
    string OrderCurrency,
    decimal FinalDiscountAmount,
    string FinalDiscountCurrency
) : ICommand<ApplicationOperationResult<VoucherDto>>;
