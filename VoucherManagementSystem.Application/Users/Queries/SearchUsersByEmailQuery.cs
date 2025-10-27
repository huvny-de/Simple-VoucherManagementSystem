using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Users.DTOs;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Users.Queries;

public class SearchUsersByEmailQuery : IRequest<ApplicationOperationResult<IEnumerable<UserDto>>>
{
    public string Email { get; set; } = string.Empty;
}

public class SearchUsersByEmailQueryHandler : IRequestHandler<SearchUsersByEmailQuery, ApplicationOperationResult<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public SearchUsersByEmailQueryHandler(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<ApplicationOperationResult<IEnumerable<UserDto>>> Handle(SearchUsersByEmailQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.SearchUsersAsync(request.Email);
        var userDtos = users.Select(user => user.ToDto(_userService));

        return ApplicationOperationResult<IEnumerable<UserDto>>.Success(userDtos);
    }
}
