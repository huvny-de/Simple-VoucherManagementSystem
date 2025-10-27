using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Promotions.Queries;

public class GetPromotionsByUserIdQueryHandler : IRequestHandler<GetPromotionsByUserIdQuery, ApplicationOperationResult<IEnumerable<PromotionDto>>>
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IPromotionService _promotionService;

    public GetPromotionsByUserIdQueryHandler(IPromotionRepository promotionRepository, IPromotionService promotionService)
    {
        _promotionRepository = promotionRepository;
        _promotionService = promotionService;
    }

    public async Task<ApplicationOperationResult<IEnumerable<PromotionDto>>> Handle(GetPromotionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var promotions = await _promotionRepository.GetPromotionsByUserIdAsync(request.UserId);
            var promotionDtos = promotions.Select(promotion => promotion.ToDto(_promotionService));

            return ApplicationOperationResult<IEnumerable<PromotionDto>>.Success(promotionDtos);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<IEnumerable<PromotionDto>>.Failure($"Failed to get promotions by user: {ex.Message}");
        }
    }
}
