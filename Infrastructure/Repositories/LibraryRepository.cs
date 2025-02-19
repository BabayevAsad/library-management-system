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
            .Where(l => !l.IsDeleted && l.Id == id)
            .Select(l => new Library
            {
                Id = l.Id,
                Name = l.Name,
                Books = l.LibraryBooks
                    .Where(lb => !lb.IsDeleted)
                    .Select(lb => lb.Book)
                    .Where(b => !b.IsDeleted)
                    .ToList()
            })
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException("Library not found.");
        
        return library;
    }
   
}