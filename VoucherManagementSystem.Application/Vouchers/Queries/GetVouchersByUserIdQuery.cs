using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Vouchers.DTOs;

namespace VoucherManagementSystem.Application.Vouchers.Queries;

public record GetVouchersByUserIdQuery(string UserId) : IQuery<ApplicationOperationResult<IEnumerable<VoucherDto>>>;
