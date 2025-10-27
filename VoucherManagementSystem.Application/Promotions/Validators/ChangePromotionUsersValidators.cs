using FluentValidation;
using VoucherManagementSystem.Application.Promotions.Commands;

namespace VoucherManagementSystem.Application.Promotions.Validators;

public class AddUserToPromotionCommandValidator : AbstractValidator<AddUserToPromotionCommand>
{
    public AddUserToPromotionCommandValidator()
    {
        RuleFor(x => x.PromotionId)
            .NotEmpty().WithMessage("Promotion ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}

public class RemoveUserFromPromotionCommandValidator : AbstractValidator<RemoveUserFromPromotionCommand>
{
    public RemoveUserFromPromotionCommandValidator()
    {
        RuleFor(x => x.PromotionId)
            .NotEmpty().WithMessage("Promotion ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}
