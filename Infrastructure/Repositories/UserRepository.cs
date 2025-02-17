using Api.User;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DbSet<User> _dbSet;
    
    public UserRepository(DataContext dataContext) : base(dataContext)
    {
        _dbSet = dataContext.Set<User>();
    }

    public async Task<User?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.UserName.Equals(name));
    }
}