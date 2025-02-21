using Api.Base;
using Api.Books;
using Api.Libraries;

namespace Api.LibraryBook;

public class LibraryBook : BaseEntity
{
    public Library Library { get; set; }
    public int LibraryId { get; set; }
    public Book Book { get; set; }
    public int BookId { get; set; }
}