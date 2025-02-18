using Api.Base;
using Api.Books;

namespace Api.People;

public class Person : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FatherName { get; set; }
    public DateTime BirthDate { get; set; }
    public int GenderId { get; set; }
    public string FinNumber { get; set; }
    public List<Book> Books { get; set; } = new List<Book>();
    public ICollection<PersonBook.PersonBook> PersonBooks { get; set; }
}