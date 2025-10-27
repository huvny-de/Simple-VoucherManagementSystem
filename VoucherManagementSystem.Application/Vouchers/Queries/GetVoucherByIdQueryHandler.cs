using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Vouchers.DTOs;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Vouchers.Queries;

public class GetVoucherByIdQueryHandler : IRequestHandler<GetVoucherByIdQuery, ApplicationOperationResult<VoucherDto?>>
{
    private readonly IVoucherRepository _voucherRepository;
    private readonly IVoucherService _voucherService;

    public GetVoucherByIdQueryHandler(IVoucherRepository voucherRepository, IVoucherService voucherService)
    {
        _voucherRepository = voucherRepository;
        _voucherService = voucherService;
    }

    public async Task<ApplicationOperationResult<VoucherDto?>> Handle(GetVoucherByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var voucher = await _voucherRepository.GetByIdAsync(request.Id);
            if (voucher == null)
            {
                return ApplicationOperationResult<VoucherDto?>.Success(null);
            }

            var voucherDto = voucher.ToDto();

            return ApplicationOperationResult<VoucherDto?>.Success(voucherDto);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<VoucherDto?>.Failure($"Failed to get voucher: {ex.Message}");
        }
    }
}
