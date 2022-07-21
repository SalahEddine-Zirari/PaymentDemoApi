using PaymentDemoApi.Core.IConfiguration;
using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.Services
{
    public class CoOwnerService:ICoOwnerService
    {
        //private readonly  ICoOwnerRepository _coOwnerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CoOwnerService(IUnitOfWork unitOfWork)
        {
            //_coOwnerRepository = coOwnerRepository;
            _unitOfWork = unitOfWork;
        }

   

        public async Task<IEnumerable<CoOwner>> GetAllCo()
        {
            //return await _coOwnerRepository.GetAll();
            return await _unitOfWork.CoOwner.GetAll();
            
        }

      
    }
}
