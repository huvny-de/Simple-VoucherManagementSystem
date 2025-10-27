using MediatR;
using Microsoft.AspNetCore.Mvc;
using VoucherManagementSystem.Application.Common.Models;

namespace VoucherManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    protected BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected IActionResult HandleResult<T>(ApplicationOperationResult<T> ApplicationOperationResult)
    {
        if (ApplicationOperationResult.IsSuccess)
        {
            return Ok(ApplicationOperationResult.Value);
        }

        return BadRequest(ApplicationOperationResult.Error);
    }

    protected IActionResult HandleResult(ApplicationOperationResult ApplicationOperationResult)
    {
        if (ApplicationOperationResult.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(ApplicationOperationResult.Error);
    }
}
