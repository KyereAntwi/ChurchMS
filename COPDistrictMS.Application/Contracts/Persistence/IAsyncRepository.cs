namespace COPDistrictMS.Application;

public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task SaveAsync();
    Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size);
    Task<int> Count();
}
