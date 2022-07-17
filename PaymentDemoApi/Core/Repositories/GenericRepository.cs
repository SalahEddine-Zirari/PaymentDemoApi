using Microsoft.EntityFrameworkCore;
using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected PaymentDemoContext _context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(PaymentDemoContext context, ILogger logger )
        {
            _context= context;
            _logger= logger;
           this.dbSet= context.Set<T>();
        }
       

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task Delete(int id)
        {
            return await dbSet.Remove(id); 
        }

        public Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
