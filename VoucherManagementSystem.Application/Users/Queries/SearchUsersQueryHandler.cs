using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;
using VoucherManagementSystem.Application.Users.DTOs;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Users.Queries;

public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, ApplicationOperationResult<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public SearchUsersQueryHandler(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<ApplicationOperationResult<IEnumerable<UserDto>>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository.SearchUsersAsync(request.SearchTerm);
            var userDtos = users.Select(user => user.ToDto(_userService));

            return ApplicationOperationResult<IEnumerable<UserDto>>.Success(userDtos);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<IEnumerable<UserDto>>.Failure($"Failed to search users: {ex.Message}");
        }
    }
}
