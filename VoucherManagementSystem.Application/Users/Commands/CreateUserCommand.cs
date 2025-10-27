using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Users.DTOs;

namespace VoucherManagementSystem.Application.Users.Commands;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth,
    string Address
) : ICommand<ApplicationOperationResult<UserDto>>;
