using Api.PersonBook;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PersonBookRepository : BaseRepository<PersonBook>, IPersonBookRepository
{
    private readonly DbSet<PersonBook> _dbSet;

    public PersonBookRepository(DataContext dataContext) : base(dataContext)
    {
        _dbSet = dataContext.Set<PersonBook>();
    }
}