using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Behavior;

public interface IPipelineBehavior<TRequest, TResponse>
{ 
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
}