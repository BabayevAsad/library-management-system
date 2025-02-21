using Api.People;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    private readonly DbSet<Person> _dbSet;

    public PersonRepository(DataContext dataContext) : base(dataContext)
    {
        _dbSet = dataContext.Set<Person>();
    }

    public async Task<Person?> GetByNameAsync(string name)
    {
        var result = await _dbSet.FirstOrDefaultAsync(p => p.Name.Equals(name));

        return result ?? throw new InvalidOperationException("Person not found.");
    }

    public async Task<Person> GetByIdAsync(int id)
    {
        var person = await _dbSet
            .Where(p => !p.IsDeleted && p.Id == id)
            .Select(p => new Person
            {
                Id = p.Id,
                Name = p.Name,
                Books = p.PersonBooks
                    .Where(pb => !pb.IsDeleted)
                    .Select(pb => pb.Book)
                    .Where(b => !b.IsDeleted)
                    .ToList()
            })
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException($"Person with ID {id} not found.");

        return person;
    }
}