using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Users.DTOs;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Users.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApplicationOperationResult<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public GetAllUsersQueryHandler(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<ApplicationOperationResult<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = users.Select(user => user.ToDto(_userService));

            return ApplicationOperationResult<IEnumerable<UserDto>>.Success(userDtos);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<IEnumerable<UserDto>>.Failure($"Failed to get users: {ex.Message}");
        }
    }
}
