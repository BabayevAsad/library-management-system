using Api.Base;

namespace Api.User;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByNameAsync(string name);
}