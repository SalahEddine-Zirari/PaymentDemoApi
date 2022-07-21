
namespace PaymentDemoApi.IRepositories
{
    public interface IMonthDetailRepository : IGenericRepository<MonthDetail>
    {
        Task<IEnumerable<MonthDetail>> GetRowsByCoOwnerId(int id);
        Task<IEnumerable<MonthDetail>> GetAllTransByCo(int id);
        Task<bool> DeleteAllTransByCo(IEnumerable<MonthDetail> TransToDelete);


    }
}
