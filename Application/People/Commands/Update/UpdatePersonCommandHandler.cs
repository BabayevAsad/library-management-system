using Api.People;
using MediatR;

namespace Application.People.Commands.Update;

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
{
    private readonly IPersonRepository _repo;

    public UpdatePersonCommandHandler(IPersonRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _repo.GetByIdAsync(request.Id);

        person.Name = request.Name;
        person.Surname = request.Surname;
        person.FatherName = request.FatherName;
        person.BirthDate = request.BirthDate;
        person.FinNumber = request.FinNumber;

        await _repo.UpdateAsync(person);
    }
}