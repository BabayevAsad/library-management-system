using Api.People;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.People.Commands.Update;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
{
    private readonly IPersonRepository _repo;
    private readonly IDistributedCache _cache;

    public UpdatePersonCommandHandler(IPersonRepository repo, IDistributedCache cache)
    {
        _repo = repo;
        _cache = cache;
    }

    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync($"{nameof(Person)}-{request.Id}");
        
        var person = await _repo.GetByIdAsync(request.Id);

        person.Name = request.Name;
        person.Surname = request.Surname;
        person.FatherName = request.FatherName;
        person.BirthDate = request.BirthDate;
        person.FinNumber = request.FinNumber;

        await _repo.UpdateAsync(person);
    }
}