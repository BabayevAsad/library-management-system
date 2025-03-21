using Api.Libraries;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Library.Commands.Update;

public class UpdateLibraryCommandHandler : IRequestHandler<UpdateLibraryCommand>
{
    private ILibraryRepository _repo;
    private readonly IDistributedCache _cache;

    public UpdateLibraryCommandHandler(ILibraryRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }
    
    public async Task Handle(UpdateLibraryCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync($"{nameof(Library)}-{request.Id}");
        
        var library = await _repo.GetByIdAsync(request.Id);

        if (library == null)
        {
            throw new NullReferenceException("Library with that Id is not exist");
        }

        library.Name = request.Name;
        library.Location = request.Location;
        library.Count = request.Count;

        await _repo.UpdateAsync(library);
    }
}