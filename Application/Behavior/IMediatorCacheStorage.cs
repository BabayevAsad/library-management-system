using System.Threading.Tasks;
using MediatR;

namespace Application.Behavior;

public interface IMediatorCacheStorage<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Get(TRequest request);

    Task Set(TRequest request, TResponse value);

    Task Remove(string cacheKeyIdentifier);

    Task RemovePatternAsync(string keyPattern);
}
