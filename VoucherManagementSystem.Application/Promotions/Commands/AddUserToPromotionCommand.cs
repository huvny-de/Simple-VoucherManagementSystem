using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;

namespace VoucherManagementSystem.Application.Promotions.Commands;

public record AddUserToPromotionCommand(string PromotionId, string UserId) : ICommand<ApplicationOperationResult>;
