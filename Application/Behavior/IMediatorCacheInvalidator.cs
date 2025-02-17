using Api.Base;

namespace Application.Behavior;

public interface IMediatorCacheInvalidator<TRequest> where TRequest : BaseEntity
{
    Task Invalidate(TRequest request);
}