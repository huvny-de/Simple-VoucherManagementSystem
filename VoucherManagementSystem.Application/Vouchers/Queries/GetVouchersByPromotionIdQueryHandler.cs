using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Vouchers.DTOs;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Vouchers.Queries;

public class GetVouchersByPromotionIdQueryHandler : IRequestHandler<GetVouchersByPromotionIdQuery, ApplicationOperationResult<IEnumerable<VoucherDto>>>
{
    private readonly IVoucherRepository _voucherRepository;

    public GetVouchersByPromotionIdQueryHandler(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<ApplicationOperationResult<IEnumerable<VoucherDto>>> Handle(GetVouchersByPromotionIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var vouchers = await _voucherRepository.GetByPromotionIdAsync(request.PromotionId);
            var voucherDtos = vouchers.Select(voucher => voucher.ToDto());

            return ApplicationOperationResult<IEnumerable<VoucherDto>>.Success(voucherDtos);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<IEnumerable<VoucherDto>>.Failure($"Failed to get vouchers by promotion: {ex.Message}");
        }
    }
}
