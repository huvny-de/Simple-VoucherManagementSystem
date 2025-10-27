using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Users.DTOs;

namespace VoucherManagementSystem.Application.Users.Commands;

public record UpdateUserCommand(
    string Id,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Address
) : ICommand<ApplicationOperationResult<UserDto>>;
