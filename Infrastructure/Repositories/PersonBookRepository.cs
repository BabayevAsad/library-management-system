using Api.PersonBook;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PersonBookRepository : BaseRepository<PersonBook>, IPersonBookRepository
{
    private readonly DbSet<PersonBook> _dbSet;
    private readonly DataContext _dataContext;

    public PersonBookRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
        _dbSet = dataContext.Set<PersonBook>();
    }
    
    public async Task<PersonBook?> GetByPersonIdAsync(int personId, int bookId)
    {
        var personBook = await _dbSet
            .Where(pb => pb.PersonId == personId && pb.BookId == bookId && !pb.IsDeleted) 
            .Include(pb => pb.Book)
            .Include(pb => pb.Person)
            .FirstOrDefaultAsync();

        if (personBook == null)
        {
            throw new InvalidOperationException($"No active records found for PersonId {personId} and BookId {bookId}");
        }

        return personBook;
    }


    public async Task DeleteAsync(PersonBook person_Book)
    {
        var personBook = await _dbSet
            .Where(pb => pb.PersonId == person_Book.PersonId && pb.BookId == person_Book.BookId)
            .FirstOrDefaultAsync();

        if (personBook != null) 
        {
            personBook.IsDeleted = true;
            await _dataContext.SaveChangesAsync(); 
        }
        
    }
}