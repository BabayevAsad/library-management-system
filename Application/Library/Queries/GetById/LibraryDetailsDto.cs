using Application.Books.Queries;

namespace Application.Library.Queries.GetById;

public class LibraryDetailsDto : LibraryDto
{
    public List<BookDto> Books { get; set; }
}