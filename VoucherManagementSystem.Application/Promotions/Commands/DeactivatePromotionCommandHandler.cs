using MediatR;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Promotions.Commands;

public class DeactivatePromotionCommandHandler : IRequestHandler<DeactivatePromotionCommand, ApplicationOperationResult>
{
    private readonly IPromotionRepository _promotionRepository;

    public DeactivatePromotionCommandHandler(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task<ApplicationOperationResult> Handle(DeactivatePromotionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var promotion = await _promotionRepository.GetByIdAsync(request.Id);
            if (promotion == null)
            {
                return ApplicationOperationResult.Failure("Promotion not found");
            }

            await _promotionRepository.UpdateAsync(promotion);

            return ApplicationOperationResult.Success();
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult.Failure($"Failed to deactivate promotion: {ex.Message}");
        }
    }
}
