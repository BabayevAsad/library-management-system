using System.Linq.Expressions;
using Api.Base;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DataContext _dataContext;
    private readonly DbSet<TEntity> _dbSet;
    
    protected BaseRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
        _dbSet = dataContext.Set<TEntity>();

    } 
        
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.Where(p => !p.IsDeleted).ToListAsync();
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
    
    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.AnyAsync(x => x.Id == id);
    }
    
    public  bool Exists(int id)
    {
        return  _dbSet.Any(x => x.Id == id);
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.Where(p => !p.IsDeleted && p.Id == id)
                   .FirstOrDefaultAsync() 
               ?? throw new NullReferenceException($"No {typeof(TEntity).Name} found with Id {id} in the database.");
    }


    public async Task CreatAsync(TEntity entity)
    {
        _dbSet.Add(entity);

        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
    
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        Console.WriteLine(entity);
       await _dataContext.SaveChangesAsync();
    }
    
}