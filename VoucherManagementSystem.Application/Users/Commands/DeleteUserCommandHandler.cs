using MediatR;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Domain.Interfaces;

namespace VoucherManagementSystem.Application.Users.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApplicationOperationResult>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApplicationOperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null)
            {
                return ApplicationOperationResult.Failure("User not found");
            }

            // Use repository's soft delete method
            await _userRepository.DeleteAsync(request.Id);

            return ApplicationOperationResult.Success();
        }
        catch (Exception ex)
        {
            return ApplicationOperationResult.Failure($"Failed to delete user: {ex.Message}");
        }
    }
}
