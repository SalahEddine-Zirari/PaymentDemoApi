using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentDemoApi.Core.IConfiguration;
using PaymentDemoApi.Core.Services.Interfaces;

namespace PaymentDemoApi.Controllers
{
    //use services


    [Route("api/PaymentsTracking")]
    [ApiController]
    public class PaymentsTrackingController : ControllerBase
    {

        private readonly ILogger<PaymentsTrackingController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMonthDetailService _monthDetailService;
        public PaymentsTrackingController(ILogger<PaymentsTrackingController> logger, IUnitOfWork unitOfWork,IMonthDetailService monthDetailService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _monthDetailService= monthDetailService;
        }


        [HttpGet]
        public async Task<IEnumerable<MonthDetail>> GetAll() =>await _monthDetailService.GetAll();
       


        [HttpGet("{TransactionId}")]
      
        public async Task<MonthDetail> GetTrasanction(int TransactionId) =>await _monthDetailService.GetTrasanction(TransactionId);


        [HttpGet("{CoOwnerId}/TransactionId")]
        public async Task<IEnumerable<MonthDetail>> GetByCoOwnerId(int CoOwnerId) => await _monthDetailService.GetByCoOwnerId(CoOwnerId);
       


        [HttpPost("details")]
        public async Task<string> AddMonthDetail(int CoOwnerId, decimal AmmountPaid) => await _monthDetailService.AddMonthDetail(CoOwnerId, AmmountPaid);
     

    }


}
