using FluentValidation;
using VoucherManagementSystem.Application.Vouchers.Commands;

namespace VoucherManagementSystem.Application.Vouchers.Validators;

public class UseVoucherCommandValidator : AbstractValidator<UseVoucherCommand>
{
    public UseVoucherCommandValidator()
    {
        RuleFor(x => x.VoucherId)
            .NotEmpty().WithMessage("Voucher ID is required.");

        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Order ID is required.");
    }
}
