using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;

namespace VoucherManagementSystem.Application.Promotions.Queries;

public record GetPromotionsByUserIdQuery(string UserId) : IQuery<ApplicationOperationResult<IEnumerable<PromotionDto>>>;
