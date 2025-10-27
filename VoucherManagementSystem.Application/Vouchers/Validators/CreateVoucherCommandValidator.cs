using FluentValidation;
using VoucherManagementSystem.Application.Vouchers.Commands;

namespace VoucherManagementSystem.Application.Vouchers.Validators;

public class CreateVoucherCommandValidator : AbstractValidator<CreateVoucherCommand>
{
    public CreateVoucherCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.PromotionId)
            .NotEmpty().WithMessage("Promotion ID is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Voucher code is required.")
            .MaximumLength(50).WithMessage("Voucher code cannot exceed 50 characters.");

        RuleFor(x => x.DiscountAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount amount cannot be negative.");

        RuleFor(x => x.DiscountCurrency)
            .NotEmpty().WithMessage("Discount currency is required.")
            .Length(3).WithMessage("Discount currency must be a 3-letter ISO code.");

        RuleFor(x => x.OrderAmount)
            .GreaterThan(0).WithMessage("Order amount must be greater than 0.");

        RuleFor(x => x.OrderCurrency)
            .NotEmpty().WithMessage("Order currency is required.")
            .Length(3).WithMessage("Order currency must be a 3-letter ISO code.");

        RuleFor(x => x.FinalDiscountAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Final discount amount cannot be negative.");

        RuleFor(x => x.FinalDiscountCurrency)
            .NotEmpty().WithMessage("Final discount currency is required.")
            .Length(3).WithMessage("Final discount currency must be a 3-letter ISO code.");
    }
}
