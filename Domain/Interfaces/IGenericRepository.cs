namespace Domain.Interfaces
{
    public interface IGenericRepository<T> where T : IEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task CreateRangeAsync(IEnumerable<T> entities);
        Task<List<T>> ListAsync();
        Task<int> SaveChangesAsync();
    }
}
