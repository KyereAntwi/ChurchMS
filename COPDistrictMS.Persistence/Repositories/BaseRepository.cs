using COPDistrictMS.Application;
using COPDistrictMS.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace COPDistrictMS.Persistence.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    private readonly COPDistrictMSContext _dbContext;

    public BaseRepository(COPDistrictMSContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<int> Count()
    {
        return await _dbContext.Set<T>().CountAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        T? t = await _dbContext.Set<T>().FindAsync(id);
        return t;
    }

    public async Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size)
    {
        return await _dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}