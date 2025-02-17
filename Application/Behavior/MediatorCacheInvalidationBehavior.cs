using Api.Base;
using MediatR;

namespace Application.Behavior
{
    public class MediatorCacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : BaseEntity, IRequest<TResponse>
    {
        private readonly List<IMediatorCacheInvalidator<TRequest>> _cacheInvalidators;

        public MediatorCacheInvalidationBehavior(
            IEnumerable<IMediatorCacheInvalidator<TRequest>> cacheInvalidators)
        {
            _cacheInvalidators = cacheInvalidators.ToList();
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            TResponse result = await next();

            foreach (IMediatorCacheInvalidator<TRequest> cacheInvalidator in _cacheInvalidators)
            {
                await cacheInvalidator.Invalidate(request);
            }

            return result;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            throw new System.NotImplementedException();
        }
    }
}