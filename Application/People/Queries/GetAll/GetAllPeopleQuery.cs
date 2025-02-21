using MediatR;

namespace Application.People.Queries.GetAll;

public class GetAllPeopleQuery : IRequest<List<PersonListDto>>
{
}