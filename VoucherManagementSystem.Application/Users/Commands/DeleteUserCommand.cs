using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;

namespace VoucherManagementSystem.Application.Users.Commands;

public record DeleteUserCommand(string Id) : ICommand<ApplicationOperationResult>;
