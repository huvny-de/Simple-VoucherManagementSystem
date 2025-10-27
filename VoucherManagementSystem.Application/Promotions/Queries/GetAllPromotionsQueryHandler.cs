using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Promotions.Queries;

public class GetAllPromotionsQueryHandler : IRequestHandler<GetAllPromotionsQuery, ApplicationOperationResult<IEnumerable<PromotionDto>>>
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IPromotionService _promotionService;

    public GetAllPromotionsQueryHandler(IPromotionRepository promotionRepository, IPromotionService promotionService)
    {
        _promotionRepository = promotionRepository;
        _promotionService = promotionService;
    }

    public async Task<ApplicationOperationResult<IEnumerable<PromotionDto>>> Handle(GetAllPromotionsQuery request, CancellationToken cancellationToken)
    {
        var promotions = await _promotionRepository.GetAllAsync();
        var promotionDtos = promotions.ToDto(_promotionService);

        return ApplicationOperationResult<IEnumerable<PromotionDto>>.Success(promotionDtos);
    }
}
