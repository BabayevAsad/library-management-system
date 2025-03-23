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
            .Include(p => p.PersonBooks)
            .ThenInclude(pb => pb.Book)
            .Where(p => !p.IsDeleted && p.Id == id)
            .FirstOrDefaultAsync();

        if (person == null)
            throw new InvalidOperationException($"Person with ID {id} not found.");

        person.PersonBooks = person.PersonBooks
            .Where(pb => !pb.IsDeleted && !pb.Book.IsDeleted)
            .ToList();

        person.Books = person.PersonBooks
            .Select(pb => pb.Book)
            .ToList();

        return person;
    }
}