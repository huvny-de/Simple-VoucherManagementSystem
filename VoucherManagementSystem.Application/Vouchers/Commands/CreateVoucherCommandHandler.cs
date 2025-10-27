using MediatR;
using VoucherManagementSystem.Application.Common.Constants;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Vouchers.DTOs;
using VoucherManagementSystem.Domain.Entities;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Vouchers.Commands;

public class CreateVoucherCommandHandler : IRequestHandler<CreateVoucherCommand, ApplicationOperationResult<VoucherDto>>
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IPromotionRepository _promotionRepository;
    private readonly IVoucherService _voucherService;
    private readonly IPromotionService _promotionService;

    public CreateVoucherCommandHandler(IVoucherRepository voucherRepository, IPromotionRepository promotionRepository, IVoucherService voucherService, IPromotionService promotionService)
    {
        _voucherRepository = voucherRepository;
        _promotionRepository = promotionRepository;
        _voucherService = voucherService;
        _promotionService = promotionService;
    }

    public async Task<ApplicationOperationResult<VoucherDto>> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
    {
        var promotion = await _promotionRepository.GetByIdAsync(request.PromotionId);
        if (promotion == null)
        {
            return ApplicationOperationResult<VoucherDto>.Failure(ErrorMessages.PromotionNotFound);
        }

        if (!_promotionService.CanBeUsedBy(promotion, request.UserId))
        {
            return ApplicationOperationResult<VoucherDto>.Failure(ErrorMessages.UserNotEligibleForPromotion);
        }

        var voucher = _voucherService.Create(
            request.UserId,
            request.PromotionId,
            request.Code,
            request.DiscountAmount,
            request.DiscountCurrency,
            request.OrderAmount,
            request.OrderCurrency,
            request.FinalDiscountAmount,
            request.FinalDiscountCurrency
        );

        var createdVoucher = await _voucherRepository.AddAsync(voucher);

        var voucherDto = createdVoucher.ToDto();

        return ApplicationOperationResult<VoucherDto>.Success(voucherDto);
    }
}
