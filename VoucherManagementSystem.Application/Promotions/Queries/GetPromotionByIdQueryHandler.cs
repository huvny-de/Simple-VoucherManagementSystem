using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Promotions.Queries;

public class GetPromotionByIdQueryHandler : IRequestHandler<GetPromotionByIdQuery, ApplicationOperationResult<PromotionDto?>>
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IPromotionService _promotionService;

    public GetPromotionByIdQueryHandler(IPromotionRepository promotionRepository, IPromotionService promotionService)
    {
        _promotionRepository = promotionRepository;
        _promotionService = promotionService;
    }

    public async Task<ApplicationOperationResult<PromotionDto?>> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var promotion = await _promotionRepository.GetByIdAsync(request.Id);
            if (promotion == null)
            {
                return ApplicationOperationResult<PromotionDto?>.Success(null);
            }

            var promotionDto = promotion.ToDto(_promotionService);

            return ApplicationOperationResult<PromotionDto?>.Success(promotionDto);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<PromotionDto?>.Failure($"Failed to get promotion: {ex.Message}");
        }
    }
}
