using Api.People;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;
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
        var result= await _dbSet.FirstOrDefaultAsync(p => p.Name.Equals(name));
        
        return result ?? throw new InvalidOperationException("Person not found.");
    }
    
    public async Task<Person> GetByIdAsync(int id)
    {
        return await _dbSet
            .Where(p => !p.IsDeleted && p.Id == id)
            .Include(p => p.Books) 
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException("Person not found.");
    }

    
}