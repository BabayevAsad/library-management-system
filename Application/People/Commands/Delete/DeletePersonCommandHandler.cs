using Api.People;
using MediatR;

namespace Application.People.Commands.Delete;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private IPersonRepository _repo;

    public DeletePersonCommandHandler(IPersonRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _repo.GetByIdAsync(request.Id);

        await _repo.DeleteAsync(person);
    }
}