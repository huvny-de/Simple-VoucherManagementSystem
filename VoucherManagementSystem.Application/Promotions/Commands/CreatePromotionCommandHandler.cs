using MediatR;
using VoucherManagementSystem.Application.Common.Constants;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;
using VoucherManagementSystem.Domain.Entities;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Promotions.Commands;

public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, ApplicationOperationResult<PromotionDto>>
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IPromotionService _promotionService;

    public CreatePromotionCommandHandler(IPromotionRepository promotionRepository, IPromotionService promotionService)
    {
        _promotionRepository = promotionRepository;
        _promotionService = promotionService;
    }

    public async Task<ApplicationOperationResult<PromotionDto>> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
    {
        var existingPromotion = await _promotionRepository.GetByCodeAsync(request.Code);
        if (existingPromotion != null)
        {
            return ApplicationOperationResult<PromotionDto>.Failure(ErrorMessages.PromotionCodeExists);
        }

        var promotion = _promotionService.Create(
            request.Name,
            request.Description,
            request.Code,
            request.DiscountAmount,
            request.DiscountCurrency,
            request.DiscountPercentage,
            request.MinimumOrderAmount,
            request.MinimumOrderCurrency,
            request.MaximumDiscountAmount,
            request.MaximumDiscountCurrency,
            request.StartDate,
            request.EndDate,
            request.UsageLimit
        );

        var createdPromotion = await _promotionRepository.AddAsync(promotion);

        var promotionDto = createdPromotion.ToDto(_promotionService);

        return ApplicationOperationResult<PromotionDto>.Success(promotionDto);
    }
}
