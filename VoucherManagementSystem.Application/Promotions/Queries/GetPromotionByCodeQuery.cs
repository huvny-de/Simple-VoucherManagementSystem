using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;

namespace VoucherManagementSystem.Application.Promotions.Queries;

public record GetPromotionByCodeQuery(string Code) : IQuery<ApplicationOperationResult<PromotionDto?>>;
