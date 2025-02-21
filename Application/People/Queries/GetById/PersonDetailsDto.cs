using Application.Books.Queries;

namespace Application.People.Queries.GetById;

public class PersonDetailsDto : PersonDto
{
    public List<BookDto> Books { get; set; }
}