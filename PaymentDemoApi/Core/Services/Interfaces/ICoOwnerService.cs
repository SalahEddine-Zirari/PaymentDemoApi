namespace PaymentDemoApi.Core.Services
{
    public interface ICoOwnerService
    {
        Task<IEnumerable<CoOwner>> GetAllCo();
        Task<CoOwner> GetCoOwnerById(int CoOwnerId);
        Task<CoOwner> AddCoOwner(string name, decimal balance, decimal monthlyFee);
        Task<string> DeleteCoOwner(int CoOwnerId);
    }
}
