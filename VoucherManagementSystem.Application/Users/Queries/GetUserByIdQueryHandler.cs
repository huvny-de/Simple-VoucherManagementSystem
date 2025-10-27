using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;
using VoucherManagementSystem.Application.Users.DTOs;
using VoucherManagementSystem.Application.Services;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Users.Queries;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApplicationOperationResult<UserDto?>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<ApplicationOperationResult<UserDto?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return ApplicationOperationResult<UserDto?>.Failure("User not found.");
            }

            var userDto = user.ToDto(_userService);

            return ApplicationOperationResult<UserDto?>.Success(userDto);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<UserDto?>.Failure($"Failed to get user: {ex.Message}");
        }
    }
}
