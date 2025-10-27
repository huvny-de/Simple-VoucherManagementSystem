using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Vouchers.DTOs;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Vouchers.Queries;

public class GetVouchersByUserIdQueryHandler : IRequestHandler<GetVouchersByUserIdQuery, ApplicationOperationResult<IEnumerable<VoucherDto>>>
{
    private readonly IVoucherRepository _voucherRepository;

    public GetVouchersByUserIdQueryHandler(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<ApplicationOperationResult<IEnumerable<VoucherDto>>> Handle(GetVouchersByUserIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var vouchers = await _voucherRepository.GetByUserIdAsync(request.UserId);
            var voucherDtos = vouchers.Select(voucher => voucher.ToDto());

            return ApplicationOperationResult<IEnumerable<VoucherDto>>.Success(voucherDtos);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<IEnumerable<VoucherDto>>.Failure($"Failed to get vouchers by user: {ex.Message}");
        }
    }
}
