using Application.Books.Queries;
using Application.People.Queries;

namespace Application.Library.Queries.GetById;

public class LibraryDetailsDto : LibraryDto
{
    public List<BookDto> Books { get; set; }
}