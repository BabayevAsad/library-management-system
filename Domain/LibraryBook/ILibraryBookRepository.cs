using Api.Base;

namespace Api.LibraryBook;

public interface ILibraryBookRepository : IBaseRepository<LibraryBook>
{
    Task<LibraryBook?> GetByLibraryIdBookIdAsync(int libraryId, int bookId);
}