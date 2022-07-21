using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.IConfiguration
{
    public interface IUnitOfWork 
    {
        ICoOwnerRepository CoOwner {get;}
        IMonthDetailRepository MonthDetail {get;}
        Task CompleteAsync();
    }
}
