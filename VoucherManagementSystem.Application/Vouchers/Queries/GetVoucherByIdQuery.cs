using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Vouchers.DTOs;

namespace VoucherManagementSystem.Application.Vouchers.Queries;

public record GetVoucherByIdQuery(string Id) : IQuery<ApplicationOperationResult<VoucherDto?>>;
