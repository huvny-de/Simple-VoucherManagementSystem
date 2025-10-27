using MediatR;
using VoucherManagementSystem.Application.Common.Constants;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Promotions.Commands;

public class UpdatePromotionCommandHandler : IRequestHandler<UpdatePromotionCommand, ApplicationOperationResult<PromotionDto>>
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IPromotionService _promotionService;

    public UpdatePromotionCommandHandler(IPromotionRepository promotionRepository, IPromotionService promotionService)
    {
        _promotionRepository = promotionRepository;
        _promotionService = promotionService;
    }

    public async Task<ApplicationOperationResult<PromotionDto>> Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
    {
        var promotion = await _promotionRepository.GetByIdAsync(request.Id);
        if (promotion == null)
        {
            return ApplicationOperationResult<PromotionDto>.Failure(ErrorMessages.PromotionNotFound);
        }

        promotion.Name = request.Name;
        promotion.Description = request.Description;
        promotion.DiscountAmount = request.DiscountAmount;
        promotion.DiscountCurrency = request.DiscountCurrency;
        promotion.DiscountPercentage = request.DiscountPercentage;
        promotion.MinimumOrderAmount = request.MinimumOrderAmount;
        promotion.MinimumOrderCurrency = request.MinimumOrderCurrency;
        promotion.MaximumDiscountAmount = request.MaximumDiscountAmount;
        promotion.MaximumDiscountCurrency = request.MaximumDiscountCurrency;
        promotion.StartDate = request.StartDate;
        promotion.EndDate = request.EndDate;
        promotion.UsageLimit = request.UsageLimit;
        promotion.UpdatedAt = DateTime.UtcNow;

        await _promotionRepository.UpdateAsync(promotion);

        var promotionDto = promotion.ToDto(_promotionService);

        return ApplicationOperationResult<PromotionDto>.Success(promotionDto);
    }
}
