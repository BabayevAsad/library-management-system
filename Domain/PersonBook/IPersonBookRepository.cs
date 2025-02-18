using Api.Base;

namespace Api.PersonBook;

public interface IPersonBookRepository : IBaseRepository<PersonBook>
{
    Task<PersonBook> GetByPersonIdAsync(int personId,int bookId);
    Task DeleteAsync(PersonBook personBook);
}