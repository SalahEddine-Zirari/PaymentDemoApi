namespace PaymentDemoApi.Core.Services
{
    public interface ICoOwnerService
    {
        Task<IEnumerable<CoOwner>> GetAllCo();
    }
}
