using PaymentDemoApi.Core.IConfiguration;
using PaymentDemoApi.Core.Services.Interfaces;
using Ardalis.GuardClauses;


namespace PaymentDemoApi.Core.Services
{
    public class MonthDetailService : IMonthDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MonthDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public async Task<IEnumerable<MonthDetail>> GetAll() => await _unitOfWork.MonthDetail.GetAll(); 

        public async Task<MonthDetail> GetTrasanction(int TransactionId)
        {
            TransactionId = Guard.Against.NegativeOrZero(TransactionId);
            var RequestedTransaction = await _unitOfWork.MonthDetail.GetById(TransactionId);

            return RequestedTransaction;
           
        }

        public async Task<IEnumerable<MonthDetail>> GetByCoOwnerId(int CoOwnerId)
        {
            CoOwnerId=Guard.Against.NegativeOrZero(CoOwnerId);
            var Rows = await _unitOfWork.MonthDetail.GetRowsByCoOwnerId(CoOwnerId);

            return Rows;
        }
        public async Task<string> AddMonthDetail(int CoOwnerId, decimal AmmountPaid)
        {
            CoOwnerId = Guard.Against.NegativeOrZero(CoOwnerId);
            AmmountPaid = Guard.Against.Negative(AmmountPaid);

            var NewMonthDetail = new MonthDetail
            {
                CoOwnerId = CoOwnerId,
                MonthNum = int.Parse(DateTime.Now.ToString("MM")),
                AmmountPaid = AmmountPaid
            };

            var CoOwnerData = await _unitOfWork.CoOwner.GetById(CoOwnerId);
            if (CoOwnerData == null)
                return $"Unable to find a CoOwner with the Id: " + CoOwnerId;

            CoOwnerData.Balance = AmmountPaid + CoOwnerData.Balance - CoOwnerData.MonthlyFee;

            if (CoOwnerData.Balance < 0)
                NewMonthDetail.IsPaid = "false";
            else
                NewMonthDetail.IsPaid = "true";

            await _unitOfWork.MonthDetail.Add(NewMonthDetail);
            await _unitOfWork.CoOwner.Update(CoOwnerData);

            await _unitOfWork.CompleteAsync();

            return "Added successfully";

        }
    }
}
