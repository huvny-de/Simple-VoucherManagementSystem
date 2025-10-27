using MediatR;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Promotions.Commands;

public class RemoveUserFromPromotionCommandHandler : IRequestHandler<RemoveUserFromPromotionCommand, ApplicationOperationResult>
{
    private readonly IPromotionRepository _promotionRepository;

    public RemoveUserFromPromotionCommandHandler(IPromotionRepository promotionRepository)
    {
        _promotionRepository = promotionRepository;
    }

    public async Task<ApplicationOperationResult> Handle(RemoveUserFromPromotionCommand request, CancellationToken cancellationToken)
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
            return ApplicationOperationResult.Failure($"Failed to remove user from promotion: {ex.Message}");
        }
    }
}
