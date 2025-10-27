using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Promotions.Queries;

public class GetActivePromotionsQueryHandler : IRequestHandler<GetActivePromotionsQuery, ApplicationOperationResult<IEnumerable<PromotionDto>>>
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IPromotionService _promotionService;

    public GetActivePromotionsQueryHandler(IPromotionRepository promotionRepository, IPromotionService promotionService)
    {
        _promotionRepository = promotionRepository;
        _promotionService = promotionService;
    }

    public async Task<ApplicationOperationResult<IEnumerable<PromotionDto>>> Handle(GetActivePromotionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var promotions = await _promotionRepository.GetActivePromotionsAsync();
            var promotionDtos = promotions.Select(promotion => promotion.ToDto(_promotionService));

            return ApplicationOperationResult<IEnumerable<PromotionDto>>.Success(promotionDtos);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<IEnumerable<PromotionDto>>.Failure($"Failed to get active promotions: {ex.Message}");
        }
    }
}
