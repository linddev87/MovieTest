namespace Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : IEntity
    {
        Task<List<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> linq);

        Task<T?> GetByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task CreateRangeAsync(IEnumerable<IEntity> entities);

        Task<List<T>> ListAsync();

        Task<int> SaveChangesAsync();

        Task<Dictionary<string, T>> DictByAltKey();
    }
}