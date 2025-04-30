namespace HrApp.DAL.Repository.IRepository;

public interface IGenericRepository<T> where T : class
{
    Task<bool> DeleteRangeAsync(ICollection<T> entities);
    Task<T> GetByIdAsync(int id);
    Task<bool> SaveChangesAsync();
    IQueryable<T> GetTableNoTracking();
    IQueryable<T> GetTableAsTracking();
    Task<T> AddAsync(T entity);
    Task<bool> AddRangeAsync(ICollection<T> entities);
    Task<bool> UpdateAsync(T entity);
    Task<bool> UpdateRangeAsync(ICollection<T> entities);
    Task<bool> DeleteAsync(T entity);
}
