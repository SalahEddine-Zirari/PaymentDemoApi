using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentDemoApi.Core.IConfiguration;

namespace PaymentDemoApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsTrackingController : ControllerBase
    {

        private readonly ILogger<PaymentsTrackingController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentsTrackingController(ILogger<PaymentsTrackingController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.MonthDetail.GetAll());
        }


        [HttpGet("{CoOwnerId}")]
        public async Task<IActionResult> GetMonthDetail(int id)
        {
            var monthDetail = await _unitOfWork.MonthDetail.GetById(id);

            if (monthDetail == null)
                return NotFound();
            return Ok(monthDetail);
        }

        [HttpGet("{TransactionId}")]
        //to be solved
        //Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints.Matches: 

        public async Task<ActionResult> GetTrasanction(int TransactionId)
        {
            var RequestedTransaction = await _unitOfWork.MonthDetail.GetById(TransactionId);

            if (RequestedTransaction == null)
                return NotFound();

            return Ok(RequestedTransaction);
        }


        [HttpPost("{CoOwnerId}")]
        public async Task<IActionResult> AddMonthDetail(int CoOwnerId, decimal AmmountPaid)
        {
            var NewMonthDetail = new MonthDetail();


            NewMonthDetail.CoOwnerId = CoOwnerId;
            NewMonthDetail.MonthNum = int.Parse(DateTime.Now.ToString("MM"));
            NewMonthDetail.AmmountPaid = AmmountPaid;

            var CoOwnerData = await _unitOfWork.CoOwner.GetById(CoOwnerId);
            if (CoOwnerData == null)
                return BadRequest($"Unable to find a CoOwner with the Id: " + CoOwnerId);

            CoOwnerData.Balance = AmmountPaid + CoOwnerData.Balance - CoOwnerData.MonthlyFee;

            if (CoOwnerData.Balance < 0)
                NewMonthDetail.IsPaid = "false";
            else
                NewMonthDetail.IsPaid = "true";

            await _unitOfWork.MonthDetail.Add(NewMonthDetail);
            await _unitOfWork.CoOwner.Update(CoOwnerData);

            await _unitOfWork.CompleteAsync();

            return Ok("Added successfully");


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByCoOwner(int id)
        {
            var Result = _unitOfWork.MonthDetail.GetByCoOwnerId(id);
            if (Result == null)
                return BadRequest($"Unable to find a CoOwner with the Id: " + id);

            return Ok(Result);

        }

        //[HttpDelete]
        //public async Task<ActionResult> DeleteByCoOwnerId(int id)
        //{
        //    var CoOwnerPayments = _unitOfWork.MonthDetail.GetByCoOwnerId(id);
        //    if (CoOwnerPayments == null)
        //        return BadRequest($"Unable to find a CoOwner with the Id: " + id);



        //}


    }


}
