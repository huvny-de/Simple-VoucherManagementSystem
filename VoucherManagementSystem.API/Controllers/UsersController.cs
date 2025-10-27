using MediatR;
using Microsoft.AspNetCore.Mvc;
using VoucherManagementSystem.Application.Users.Commands;
using VoucherManagementSystem.Application.Users.Queries;

namespace VoucherManagementSystem.API.Controllers;

public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var ApplicationOperationResult = await _mediator.Send(new GetAllUsersQuery());
        return HandleResult(ApplicationOperationResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var ApplicationOperationResult = await _mediator.Send(new GetUserByIdQuery(id));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUsers([FromQuery] string searchTerm)
    {
        var ApplicationOperationResult = await _mediator.Send(new SearchUsersQuery(searchTerm));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var ApplicationOperationResult = await _mediator.Send(command);
        return HandleResult(ApplicationOperationResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
    {
        var updateCommand = command with { Id = id };
        var ApplicationOperationResult = await _mediator.Send(updateCommand);
        return HandleResult(ApplicationOperationResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var ApplicationOperationResult = await _mediator.Send(new DeleteUserCommand(id));
        return HandleResult(ApplicationOperationResult);
    }
}
