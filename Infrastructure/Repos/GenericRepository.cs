using Domain.Interfaces;

namespace Infrastructure.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DbSet<T> _dbSet;
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> linq)
        {
            return await _dbSet.Where(linq).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            var res = await _dbSet.AddAsync(entity);
            return res.Entity;
        }

        public async Task<List<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(IEnumerable<IEntity> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        public async Task<Dictionary<string, T>> DictByAltKey()
        {
            var dict = await _dbSet.ToDictionaryAsync(e => e.AlternateKey);
            return dict;
        }
    }
}
