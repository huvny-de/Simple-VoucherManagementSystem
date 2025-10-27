using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;

namespace VoucherManagementSystem.Application.Promotions.Commands;

public record DeactivatePromotionCommand(string Id) : ICommand<ApplicationOperationResult>;
