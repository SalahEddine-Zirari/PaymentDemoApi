using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.IConfiguration
{
    public interface IUnitOfWork 
    {
        ICoOwnerRepository CoOwners { get; }
        IMonthDetailRepository MonthDetails { get; }

        Task CompleteAsync();
    }
}
