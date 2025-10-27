using MediatR;
using VoucherManagementSystem.Application.Common.Constants;
using VoucherManagementSystem.Application.Common.Mappers;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Users.DTOs;
using VoucherManagementSystem.Domain.Entities;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationOperationResult<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public CreateUserCommandHandler(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    public async Task<ApplicationOperationResult<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return ApplicationOperationResult<UserDto>.Failure(ErrorMessages.UserWithEmailExists);
        }

        var user = _userService.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.DateOfBirth,
            request.Address
        );

        var createdUser = await _userRepository.AddAsync(user);
        var userDto = createdUser.ToDto(_userService);

        return ApplicationOperationResult<UserDto>.Success(userDto);
    }
}
