using Curiosity.Domain.Abstractions;
using MediatR;

namespace Curiosity.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{

}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand { }
