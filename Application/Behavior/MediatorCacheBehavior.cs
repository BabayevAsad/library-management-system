using MediatR;

namespace Application.Behavior;

public class MediatorCacheBehavior<TRequest, TResponse> : IPipelineBehavior<
    TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly List<IMediatorCacheStorage<TRequest, TResponse>> _caches;

    public MediatorCacheBehavior(
        IEnumerable<IMediatorCacheStorage<TRequest, TResponse>> cachedRequests)
    {
        this._caches = cachedRequests.ToList();
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        IMediatorCacheStorage<TRequest, TResponse> cacheRequest = _caches.FirstOrDefault();
        if (cacheRequest == null)
            return await next();
        TResponse response = await cacheRequest.Get(request);
        if ( response != null)
            return response;
        TResponse result = await next();
        await cacheRequest.Set(request, result);
        return result;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        throw new System.NotImplementedException();
    }
}
