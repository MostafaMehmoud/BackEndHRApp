using Microsoft.EntityFrameworkCore;
using HrApp.DAL.Repository.IRepository;
using HrApp.DAL.Data;

namespace HrApp.API.Repos;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{

    protected readonly HrAppDbContext _dbContext;

    public GenericRepository(HrAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {

        return await _dbContext.Set<T>().FindAsync(id);
    }


    public IQueryable<T> GetTableNoTracking()
    {
        return _dbContext.Set<T>().AsNoTracking().AsQueryable();
    }


    public virtual async Task<bool> AddRangeAsync(ICollection<T> entities)
    {
        await _dbContext.Set<T>().AddRangeAsync(entities);
        return await _dbContext.SaveChangesAsync() > 0;

    }
    public virtual async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        return await _dbContext.SaveChangesAsync() > 0;

    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }
    public virtual async Task<bool> DeleteRangeAsync(ICollection<T> entities)
    {
        foreach (var entity in entities)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }


    public IQueryable<T> GetTableAsTracking()
    {
        return _dbContext.Set<T>().AsQueryable();

    }

    public virtual async Task<bool> UpdateRangeAsync(ICollection<T> entities)
    {
        _dbContext.Set<T>().UpdateRange(entities);
        return await _dbContext.SaveChangesAsync() > 0;
    }

}
