using Api.People;
using Application.People.Queries;

namespace Application.Books.Queries.GetById;

public class BookDetailsDto : BookDto
{
    public List<PersonDto> People { get; set; }
}