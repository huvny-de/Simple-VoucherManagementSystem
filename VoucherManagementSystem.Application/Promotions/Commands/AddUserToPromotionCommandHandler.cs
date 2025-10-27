using MediatR;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Promotions.Commands;

public class AddUserToPromotionCommandHandler : IRequestHandler<AddUserToPromotionCommand, ApplicationOperationResult>
{
    private readonly IPromotionRepository _promotionRepository;

    public AddUserToPromotionCommandHandler(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task<ApplicationOperationResult> Handle(AddUserToPromotionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var promotion = await _promotionRepository.GetByIdAsync(request.PromotionId);
            if (promotion == null)
            {
                return ApplicationOperationResult.Failure("Promotion not found");
            }

            await _promotionRepository.UpdateAsync(promotion);

            return ApplicationOperationResult.Success();
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult.Failure($"Failed to add user to promotion: {ex.Message}");
        }
    }
}
