using FluentValidation;
using VoucherManagementSystem.Application.Promotions.Commands;

namespace VoucherManagementSystem.Application.Promotions.Validators;

public class UpdatePromotionCommandValidator : AbstractValidator<UpdatePromotionCommand>
{
    public UpdatePromotionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Promotion ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Promotion name is required.")
            .MaximumLength(100).WithMessage("Promotion name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Promotion description cannot exceed 500 characters.");

        RuleFor(x => x.DiscountAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount amount cannot be negative.");

        RuleFor(x => x.DiscountCurrency)
            .NotEmpty().WithMessage("Discount currency is required.")
            .Length(3).WithMessage("Discount currency must be a 3-letter ISO code.");

        RuleFor(x => x.DiscountPercentage)
            .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");

        RuleFor(x => x.MinimumOrderAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum order amount cannot be negative.");

        RuleFor(x => x.MinimumOrderCurrency)
            .NotEmpty().WithMessage("Minimum order currency is required.")
            .Length(3).WithMessage("Minimum order currency must be a 3-letter ISO code.");

        RuleFor(x => x.MaximumDiscountAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Maximum discount amount cannot be negative.");

        RuleFor(x => x.MaximumDiscountCurrency)
            .NotEmpty().WithMessage("Maximum discount currency is required.")
            .Length(3).WithMessage("Maximum discount currency must be a 3-letter ISO code.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .GreaterThan(x => x.StartDate).WithMessage("End date must be after start date.");

        RuleFor(x => x.UsageLimit)
            .GreaterThan(0).WithMessage("Usage limit must be greater than 0.");
    }
}
