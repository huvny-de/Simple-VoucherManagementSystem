using MediatR;
using Microsoft.AspNetCore.Mvc;
using VoucherManagementSystem.Application.Vouchers.Commands;
using VoucherManagementSystem.Application.Vouchers.Queries;

namespace VoucherManagementSystem.API.Controllers;

public class VouchersController : BaseController
{
    public VouchersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVoucherById(string id)
    {
        var ApplicationOperationResult = await _mediator.Send(new GetVoucherByIdQuery(id));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetVouchersByUserId(string userId)
    {
        var ApplicationOperationResult = await _mediator.Send(new GetVouchersByUserIdQuery(userId));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpGet("promotion/{promotionId}")]
    public async Task<IActionResult> GetVouchersByPromotionId(string promotionId)
    {
        var ApplicationOperationResult = await _mediator.Send(new GetVouchersByPromotionIdQuery(promotionId));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVoucher([FromBody] CreateVoucherCommand command)
    {
        var ApplicationOperationResult = await _mediator.Send(command);
        return HandleResult(ApplicationOperationResult);
    }

    [HttpPost("{voucherId}/use")]
    public async Task<IActionResult> UseVoucher(string voucherId, [FromBody] UseVoucherRequest request)
    {
        var command = new UseVoucherCommand(voucherId, request.OrderId);
        var ApplicationOperationResult = await _mediator.Send(command);
        return HandleResult(ApplicationOperationResult);
    }
}

public class UseVoucherRequest
{
    public string OrderId { get; set; } = string.Empty;
}
