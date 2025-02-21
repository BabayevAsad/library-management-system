using MediatR;

namespace Application.People.Queries.GetById;

public class GetByIdPersonQuery : BaseDto, IRequest<PersonDetailsDto>
{
}