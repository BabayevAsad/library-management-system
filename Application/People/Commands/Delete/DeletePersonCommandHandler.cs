using Api.People;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.People.Commands.Delete;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private IPersonRepository _repo;
    private readonly IDistributedCache _cache;

    public DeletePersonCommandHandler(IPersonRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync($"{nameof(Person)}-{request.Id}");
        
        var person = await _repo.GetByIdAsync(request.Id);
        await _repo.DeleteAsync(person);
    }
}