using Api.People;
using Application.Books.Queries;
using MediatR;

namespace Application.People.Commands.Create;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand,int>
{
    private IPersonRepository _repo;

    public CreatePersonCommandHandler(IPersonRepository repository)
    {
        _repo = repository;
    }

    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person()
        {
            Name = request.Name,
            Surname = request.Surname,
            FatherName = request.FatherName,
            BirthDate = request.BirthDate,
            GenderId = (int)GenderHelper.GetById(request.GenderId),
            FinNumber = request.FinNumber,
        };

        await _repo.CreatAsync(person);

        return person.Id;
    }
}