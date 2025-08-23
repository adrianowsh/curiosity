using Curiosity.Domain.Abstractions;
using MediatR;

namespace Curiosity.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

public interface IQuery
{
}
