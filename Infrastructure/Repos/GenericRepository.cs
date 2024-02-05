using Domain.Interfaces;

namespace Infrastructure.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public List<T> List()
        {
            return _dbSet.ToList();
        }
    }
}
