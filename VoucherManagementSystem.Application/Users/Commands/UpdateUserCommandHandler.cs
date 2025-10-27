using MediatR;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Users.DTOs;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApplicationOperationResult<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<ApplicationOperationResult<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return ApplicationOperationResult<UserDto>.Failure("User not found");
            }

            _userService.UpdateProfile(user, request.FirstName, request.LastName, request.PhoneNumber, request.Address);

            await _userRepository.UpdateAsync(user);

            var userDto = user.ToDto(_userService);

            return ApplicationOperationResult<UserDto>.Success(userDto);
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult<UserDto>.Failure($"Failed to update user: {ex.Message}");
        }
    }
}
