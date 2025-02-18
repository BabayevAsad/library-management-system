using Api.Base;
using Api.Books;
using Api.People;

namespace Api.PersonBook;

public class PersonBook : BaseEntity
{
    public Person Person { get; set; }
    public int PersonId { get; set; }
    
    public Book Book { get; set; }
    public int BookId { get; set; }

    public PersonBook()
    {
        
    }
    public PersonBook(int personId, int bookId)
    {
        PersonId = personId;
        BookId = bookId;
    }
    
}