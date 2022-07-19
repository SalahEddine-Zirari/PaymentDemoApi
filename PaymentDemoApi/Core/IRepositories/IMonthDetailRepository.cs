
namespace PaymentDemoApi.IRepositories
{
    public interface IMonthDetailRepository : IGenericRepository<MonthDetail>
    {
        Task<IEnumerable<MonthDetail>> GetByCoOwnerId(int id);  


    }
}
