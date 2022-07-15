using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PaymentDemoApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsTrackingController : ControllerBase
    {
        

        private readonly PaymentDemoContext _context;
        public PaymentsTrackingController(PaymentDemoContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<MonthDetail>>> GetMonthDetails() => Ok(await _context.MonthDetails.ToListAsync());
       

        [HttpGet("{CoOwnerId}")]
        public  async Task<ActionResult<List<MonthDetail>>> GetMonthDetails(int CoOwnerId)
        {
            var RequestedRow = await _context.MonthDetails.Where(x => x.CoOwnerId == CoOwnerId).ToListAsync();
            if (RequestedRow == null)
                return NotFound();

            return Ok(RequestedRow);
        }

        [HttpGet("{TransactionId}")]
        //to be solved
        //Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints.Matches: 

        public async Task<ActionResult> GetTrasanction(int TransactionId)
        {
            var RequestedTransaction = await _context.MonthDetails.FirstAsync(x => x.TransactionId == TransactionId);

            if (RequestedTransaction == null)
                return NotFound();

            return Ok(RequestedTransaction);
        }


        [HttpPost("{CoOwnerId}")]
        public async Task<ActionResult> AddMonthDetail(int CoOwnerId,decimal AmmountPaid)
        {
            var NewMonthDetails = new MonthDetail();


            NewMonthDetails.CoOwnerId = CoOwnerId;
            NewMonthDetails.MonthNum = int.Parse(DateTime.Now.ToString("MM"));
            NewMonthDetails.AmmountPaid = AmmountPaid;

            if(AmmountPaid<0)
            {

            }

            var CoOwnerData =await _context.CoOwners.FirstOrDefaultAsync(x=>x.Id==CoOwnerId);  
            if(CoOwnerData == null)
                return BadRequest($"Unable to find a CoOwner with the Id: " + CoOwnerId);

            CoOwnerData.Balance = AmmountPaid + CoOwnerData.Balance - CoOwnerData.MonthlyFee;

            if (CoOwnerData.Balance < 0)
                NewMonthDetails.IsPaid = "false";          
            else 
                NewMonthDetails.IsPaid = "true";

            _context.MonthDetails.Add(NewMonthDetails);
            _context.CoOwners.Update(CoOwnerData);

            await _context.SaveChangesAsync();

            return Ok("Added successfully");
            
            
        }
    }

    
}
