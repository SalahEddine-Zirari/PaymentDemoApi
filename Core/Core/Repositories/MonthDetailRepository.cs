using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.Repositories
{
    public class MonthDetailRepository : GenericRepository<MonthDetail>, IMonthDetailRepository
    {
        public MonthDetailRepository(PaymentDemoContext _context, ILogger logger) : base(_context, logger)
        {
        }

        public override async Task<IEnumerable<MonthDetail>> GetAll()
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

        public bool Add(MonthDetail entity)
        {

            try
            {
                dbSet.Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Add method error", typeof(MonthDetailRepository));
                return false;
            }
        }


        public override async Task<bool> Delete(int id)
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

        

        public override async Task<bool> Update(MonthDetail entity)
        {
            try
            {

                dbSet.Update(entity);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update method error", typeof(MonthDetailRepository));
                return false;
            }
        }

        public async Task<IEnumerable<MonthDetail>> GetRowsByCoOwnerId(int CoOwnerId)
        {
            try
            {
                return await dbSet.Where(x => x.CoOwnerId == CoOwnerId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetRowByCoOwnerId method error", typeof(MonthDetailRepository));
                return new List<MonthDetail>();
            }
        }



        public async Task<IEnumerable<MonthDetail>> GetAllTransByCo(int CoOwnerId)
        {
            try
            {
                return await dbSet.Where(x => x.CoOwnerId == CoOwnerId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllTransByCo method error", typeof(MonthDetailRepository));
                return new List<MonthDetail>();
            }
        }
        public async Task<bool> DeleteAllTransByCo(IEnumerable<MonthDetail> TransToDelete)
        {
            try
            {
            dbSet.RemoveRange(TransToDelete);
            return true;

            }
            catch(Exception e)
            {
                _logger.LogError(e,"{Repo}Delete method error", typeof(MonthDetailRepository));
                return false;
            }

        }

    }
}
