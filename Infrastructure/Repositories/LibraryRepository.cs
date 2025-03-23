using Api.Libraries;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LibraryRepository : BaseRepository<Library>, ILibraryRepository
{
    private readonly DbSet<Library> _dbSet;
    
    public LibraryRepository(DataContext dataContext) : base(dataContext)
    {
        _dbSet = dataContext.Set<Library>();
    }

    public async Task<Library?> GetByNameAsync(string name)
    {
        var result= await _dbSet.FirstOrDefaultAsync(p => p.Name.Equals(name));
        
        return result ?? throw new InvalidOperationException("Library not found.");
    }
    
    public async Task<Library> GetByIdAsync(int id)
    {
        var library = await _dbSet
            .Include(l => l.LibraryBooks)
            .ThenInclude(lb => lb.Book)
            .Where(l => !l.IsDeleted && l.Id == id)
            .FirstOrDefaultAsync();

        if (library == null)
            throw new InvalidOperationException($"Library with ID {id} not found.");

        library.LibraryBooks = library.LibraryBooks
            .Where(lb => !lb.IsDeleted && !lb.Book.IsDeleted)
            .ToList();

        library.Books = library.LibraryBooks
            .Select(lb => lb.Book)
            .ToList();

        return library;
    }
} 