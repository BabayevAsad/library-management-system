using Application.Books.Queries;
using Application.Books.Queries.GetById;

namespace Application.People.Queries.GetById;

public class PersonDetailsDto : PersonDto
{
    public List<BookDto> Books { get; set; }
}