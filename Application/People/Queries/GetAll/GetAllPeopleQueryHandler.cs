using Api.People;
using MediatR;

namespace Application.People.Queries.GetAll;

public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, List<PersonListDto>>
{
    private IPersonRepository _repo;

    public GetAllPeopleQueryHandler(IPersonRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<PersonListDto>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        var people = await _repo.GetAllAsync();

        var dto = people.Where(p => !p.IsDeleted)
            .Select(p => new PersonListDto
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                FatherName = p.FatherName,
                BirthDate = p.BirthDate,
                GenderId = (Gender)GenderHelper.GetById(p.GenderId),
                FinNumber = p.FinNumber,
            }).ToList();

        return dto;
    }
}