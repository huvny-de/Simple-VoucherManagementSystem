using MediatR;
using Microsoft.AspNetCore.Mvc;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Users.DTOs;
using VoucherManagementSystem.Application.Users.Queries;

namespace VoucherManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : BaseController
{

    public SearchController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("users")]
    public async Task<ActionResult<ApplicationOperationResult<IEnumerable<UserDto>>>> SearchUsersByEmail([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest(ApplicationOperationResult<IEnumerable<UserDto>>.Failure("Email is required"));
        }

        var query = new SearchUsersByEmailQuery { Email = email };
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}
