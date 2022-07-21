using Microsoft.Extensions.Logging;
using PaymentDemoApi.Core.IConfiguration;
using PaymentDemoApi.Core.Services;

namespace PaymentDemoApi.Controllers

    //use services
{
    [ApiController]
    [Route("api/CoOwnerManagement")]
    public class CoOwnerManagementController : ControllerBase
    {
        private readonly ILogger<CoOwnerManagementController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICoOwnerService _CoService;

        public CoOwnerManagementController(ILogger<CoOwnerManagementController> logger,IUnitOfWork unitOfWork, ICoOwnerService CoService)
        {
            
            _logger = logger;
            _unitOfWork = unitOfWork;
            _CoService = CoService;
        }

        [HttpGet] 
        public async Task<IEnumerable<CoOwner>> GetAll() =>await _CoService.GetAllCo();


        [HttpGet("{CoOwnerId}")]
        public async Task<CoOwner> GetCoOwner(int CoOwnerId) =>  await _CoService.GetCoOwnerById(CoOwnerId);
       

        [HttpPost("details")]
        
        public async Task<CoOwner> AddCoOwner(string name, decimal balance, decimal monthlyFee) =>await _CoService.AddCoOwner(name, balance, monthlyFee);





        [HttpDelete("{CoOwnerId}")]
        public async Task<string> DeleteCoOwner(int CoOwnerId) =>await _CoService.DeleteCoOwner(CoOwnerId);
       


    }
}
