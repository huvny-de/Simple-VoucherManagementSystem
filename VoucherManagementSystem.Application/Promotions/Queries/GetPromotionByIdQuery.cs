using MediatR;
using VoucherManagementSystem.Application.Common.Interfaces;
using VoucherManagementSystem.Application.Common.Models;
using VoucherManagementSystem.Application.Promotions.DTOs;

namespace VoucherManagementSystem.Application.Promotions.Queries;

public record GetPromotionByIdQuery(string Id) : IQuery<ApplicationOperationResult<PromotionDto?>>;
