using MediatR;

namespace VoucherManagementSystem.Application.Common.Interfaces;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
