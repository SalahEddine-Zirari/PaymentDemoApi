using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected PaymentDemoContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(PaymentDemoContext context, ILogger logger)
        {
            _context= context;
            _logger= logger;
            dbSet= context.Set<T>();
        }
        public virtual async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> Insert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
