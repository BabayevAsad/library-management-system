using Api.Base;
using Api.Books;

namespace Api.Libraries;

public class Library : BaseEntity
{
    public string Name { get; set; }
    public string Location { get; set; }
    public int Count { get; set; }
    public List<Book> Books { get; set; } = new List<Book>();
    public ICollection<LibraryBook.LibraryBook> LibraryBooks { get; set; }
}