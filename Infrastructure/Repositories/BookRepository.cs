using Api.Books;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>, IBookRepository
{
    private readonly DbSet<Book> _dbSet;
    
    public BookRepository(DataContext dataContext) : base(dataContext)
    {
        _dbSet = dataContext.Set<Book>();
    }
    
    public async Task<Book> GetByIdAsync(int Id)
    {
        return await _dbSet
            .Where(b => !b.IsDeleted && b.Id == Id)
            .Include(b => b.People) 
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException("Person not found.");
    }
}