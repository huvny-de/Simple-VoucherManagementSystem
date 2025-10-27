using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Users.DTOs;

namespace VoucherManagementSystem.Application.Users.Queries;

public record SearchUsersQuery(string SearchTerm) : IQuery<ApplicationOperationResult<IEnumerable<UserDto>>>;
