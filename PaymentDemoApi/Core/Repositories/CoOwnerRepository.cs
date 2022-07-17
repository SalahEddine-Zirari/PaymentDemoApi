using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.Repositories
{
    public class CoOwnerRepository : GenericRepository<CoOwner>,ICoOwnerRepository
    {
        public CoOwnerRepository(PaymentDemoContext context,ILogger logger):base(context, logger) {}

        public override async Task<IEnumerable<CoOwner>> GetAll()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"{Repo} All method error",typeof(CoOwnerRepository));
                return new List<CoOwner>();
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
                _logger.LogError(ex, "{Repo} Delete method error", typeof(CoOwnerRepository));
                return false;
            }
        }

        
    }
}
