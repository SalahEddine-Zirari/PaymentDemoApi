using PaymentDemoApi.Core.IConfiguration;
using PaymentDemoApi.IRepositories;


namespace PaymentDemoApi.Core.Services
{
    public class CoOwnerService : ICoOwnerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoOwnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

   
        public async Task<IEnumerable<CoOwner>> GetAllCo() => await _unitOfWork.CoOwner.GetAll();

        public async Task<CoOwner> GetCoOwnerById(int CoOwnerId)
        {
            var coOwner = await _unitOfWork.CoOwner.GetById(CoOwnerId);
            return coOwner;
        }

        public async Task<CoOwner> AddCoOwner(string name, decimal balance, decimal monthlyFee)
        {
            var coOwner = new CoOwner()
            {
                Name = name,
                Balance = balance,
                MonthlyFee = monthlyFee
            };
            
            await _unitOfWork.CoOwner.Add(coOwner);
            await _unitOfWork.CompleteAsync();

            return coOwner;
            
        }
        public async Task<string> DeleteCoOwner(int CoOwnerId)
        {
            var CoOwner = await _unitOfWork.CoOwner.GetById(CoOwnerId);

            if (CoOwner == null)
                return "inexistant CoOwner";

            var Transactions = await _unitOfWork.MonthDetail.GetAllTransByCo(CoOwnerId);

            if (Transactions != null)
            {
                await _unitOfWork.MonthDetail.DeleteAllTransByCo(Transactions);
            }
            await _unitOfWork.CoOwner.Delete(CoOwnerId);
            await _unitOfWork.CompleteAsync();

            return "deleted succesfully";
        }


    }
}
