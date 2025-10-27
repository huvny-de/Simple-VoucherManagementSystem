using MediatR;
using Microsoft.AspNetCore.Mvc;
using VoucherManagementSystem.Application.Promotions.Commands;
using VoucherManagementSystem.Application.Promotions.Queries;

namespace VoucherManagementSystem.API.Controllers;

public class PromotionsController : BaseController
{
    public PromotionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPromotions()
    {
        var ApplicationOperationResult = await _mediator.Send(new GetAllPromotionsQuery());
        return HandleResult(ApplicationOperationResult);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActivePromotions()
    {
        var ApplicationOperationResult = await _mediator.Send(new GetActivePromotionsQuery());
        return HandleResult(ApplicationOperationResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPromotionById(string id)
    {
        var ApplicationOperationResult = await _mediator.Send(new GetPromotionByIdQuery(id));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetPromotionByCode(string code)
    {
        var ApplicationOperationResult = await _mediator.Send(new GetPromotionByCodeQuery(code));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetPromotionsByUserId(string userId)
    {
        var ApplicationOperationResult = await _mediator.Send(new GetPromotionsByUserIdQuery(userId));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionCommand command)
    {
        var ApplicationOperationResult = await _mediator.Send(command);
        return HandleResult(ApplicationOperationResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePromotion(string id, [FromBody] UpdatePromotionCommand command)
    {
        var updateCommand = command with { Id = id };
        var ApplicationOperationResult = await _mediator.Send(updateCommand);
        return HandleResult(ApplicationOperationResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeactivatePromotion(string id)
    {
        var ApplicationOperationResult = await _mediator.Send(new DeactivatePromotionCommand(id));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpPost("{promotionId}/users/{userId}")]
    public async Task<IActionResult> AddUserToPromotion(string promotionId, string userId)
    {
        var ApplicationOperationResult = await _mediator.Send(new AddUserToPromotionCommand(promotionId, userId));
        return HandleResult(ApplicationOperationResult);
    }

    [HttpDelete("{promotionId}/users/{userId}")]
    public async Task<IActionResult> RemoveUserFromPromotion(string promotionId, string userId)
    {
        var ApplicationOperationResult = await _mediator.Send(new RemoveUserFromPromotionCommand(promotionId, userId));
        return HandleResult(ApplicationOperationResult);
    }
}
