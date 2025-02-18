using Api.LibraryBook;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LibraryBookRepository : BaseRepository<LibraryBook>, ILibraryBookRepository
{
    private readonly DbSet<LibraryBook> _dbSet;
    private readonly DataContext _dataContext;

    public LibraryBookRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
        _dbSet = dataContext.Set<LibraryBook>();
    }

    public async Task<LibraryBook?> GetByLibraryIdBookIdAsync(int libraryId, int bookId)
    {
        var libraryBook = await _dbSet
            .Where(pb => pb.LibraryId == libraryId && pb.BookId == bookId && !pb.IsDeleted) 
            .Include(pb => pb.Book)
            .Include(pb => pb.Library)
            .FirstOrDefaultAsync();

        if (libraryBook == null)
        {
            throw new InvalidOperationException($"No active records found for PersonId {libraryId} and BookId {bookId}");
        }

        return  libraryBook;
        
    }
    
    
    public async Task DeleteAsync(LibraryBook library_Book)
    {
        var personBook = await _dbSet
            .Where(pb => pb.LibraryId == library_Book.LibraryId && pb.BookId == library_Book.BookId)
            .FirstOrDefaultAsync();

        if (personBook != null) 
        {
            personBook.IsDeleted = true;
            await _dataContext.SaveChangesAsync(); 
        }
        
    }
    
}