using MediatR;
using VoucherManagementSystem.Application.Common.Constants;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Vouchers.Commands;

public class UseVoucherCommandHandler : IRequestHandler<UseVoucherCommand, ApplicationOperationResult>
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IPromotionRepository _promotionRepository;
    private readonly IVoucherService _voucherService;
    private readonly IPromotionService _promotionService;

    public UseVoucherCommandHandler(IVoucherRepository voucherRepository, IPromotionRepository promotionRepository, IVoucherService voucherService, IPromotionService promotionService)
    {
        _voucherRepository = voucherRepository;
        _promotionRepository = promotionRepository;
        _voucherService = voucherService;
        _promotionService = promotionService;
    }

    public async Task<ApplicationOperationResult> Handle(UseVoucherCommand request, CancellationToken cancellationToken)
    {
        var voucher = await _voucherRepository.GetByIdAsync(request.VoucherId);
        if (voucher == null)
        {
            return ApplicationOperationResult.Failure(ErrorMessages.VoucherNotFound);
        }

        if (voucher.IsUsed)
        {
            return ApplicationOperationResult.Failure(ErrorMessages.VoucherAlreadyUsed);
        }

        var promotion = await _promotionRepository.GetByIdAsync(voucher.PromotionId);
        if (promotion == null)
        {
            return ApplicationOperationResult.Failure(ErrorMessages.PromotionNotFound);
        }

        if (!_promotionService.IsValid(promotion))
        {
            return ApplicationOperationResult.Failure(ErrorMessages.PromotionNotValid);
        }

        _voucherService.Use(voucher, request.OrderId);
        _promotionService.Use(promotion);

        await _voucherRepository.UpdateAsync(voucher);
        await _promotionRepository.UpdateAsync(promotion);

        return ApplicationOperationResult.Success();
    }
}
