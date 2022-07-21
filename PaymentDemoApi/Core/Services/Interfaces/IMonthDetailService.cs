namespace PaymentDemoApi.Core.Services.Interfaces
{
    public interface IMonthDetailService
    {
        Task<IEnumerable<MonthDetail>> GetAll();
        Task<MonthDetail> GetTrasanction(int TransactionId);
        Task<IEnumerable<MonthDetail>> GetByCoOwnerId(int CoOwnerId);
        Task<string> AddMonthDetail(int CoOwnerId, decimal AmmountPaid);
    }
}
