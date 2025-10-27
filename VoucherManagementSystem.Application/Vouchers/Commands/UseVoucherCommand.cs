using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;

namespace VoucherManagementSystem.Application.Vouchers.Commands;

public record UseVoucherCommand(string VoucherId, string OrderId) : ICommand<ApplicationOperationResult>;
