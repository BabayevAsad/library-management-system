using Api.Libraries;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Library.Commands.Delete;

public class DeleteLibraryCommandHandler : IRequestHandler<DeleteLibraryCommand>
{
    private readonly ILibraryRepository _repo;
    private readonly IDistributedCache _cache;

    public DeleteLibraryCommandHandler(ILibraryRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task Handle(DeleteLibraryCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync($"{nameof(Library)}-{request.Id}");
        
        var library = await _repo.GetByIdAsync(request.Id);
        await _repo.DeleteAsync(library);
    }
}