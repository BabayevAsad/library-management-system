using Api.LibraryBook;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LibraryBookRepository : BaseRepository<LibraryBook>, ILibraryBookRepository
{
    private readonly DbSet<LibraryBook> _dbSet;

    public LibraryBookRepository(DataContext dataContext) : base(dataContext)
    {
        _dbSet = dataContext.Set<LibraryBook>();
    }
}