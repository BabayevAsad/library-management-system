using Api.Base;
using Api.Libraries;
using Api.People;

namespace Api.Books;

public class Book : BaseEntity
{
    public string Name { get; set; }
    public string Type { get; set; }
    public int PageCount { get; set; }
    public int Price { get; set; }
    
    public List<Person> People { get; set;} = new List<Person>();
    public List<Library> Libraries { get; set; } = new List<Library>();
    public ICollection<PersonBook.PersonBook> PersonBooks { get; set; }
}