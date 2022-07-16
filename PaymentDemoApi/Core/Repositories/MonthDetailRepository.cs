using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.Repositories
{
    public class MonthDetailRepository : GenericRepository<MonthDetail>, IMonthDetailRepository
    {
        public MonthDetailRepository(PaymentDemoContext _context, ILogger logger) : base(_context, logger)
        {
        }

        public override async Task<IEnumerable<MonthDetail>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error", typeof(MonthDetailRepository));
                return new List<MonthDetail>();
            }
        }

        public override async Task<bool> Add(MonthDetail entity)
        {

            try
            {
                return await Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(MonthDetailRepository));
                return false;
            }
        }


        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var exists = await dbSet.FindAsync(id);

                if (exists != null)
                {
                    dbSet.Remove(exists);
                    return true;
                }


                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete method error", typeof(MonthDetailRepository));
                return false;
            }
        }
    }
}
