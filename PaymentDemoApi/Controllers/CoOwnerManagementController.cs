using Microsoft.Extensions.Logging;
using PaymentDemoApi.Core.IConfiguration;

namespace PaymentDemoApi.Controllers
{
    [ApiController]
    [Route("api/CoOwnerManagement")]
    public class CoOwnerManagementController : ControllerBase
    {
        private readonly ILogger<CoOwnerManagementController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CoOwnerManagementController(ILogger<CoOwnerManagementController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.CoOwner.GetAll());
        }


        [HttpGet("{CoOwnerId}")]
        public async Task<IActionResult> GetCoOwner(int CoOwnerId)
        {
            var coOwner = await _unitOfWork.CoOwner.GetById(CoOwnerId);

            if (coOwner == null)
                return NotFound();
            return Ok(coOwner);
        }

        [HttpPost("details")]
        
        public async Task<ActionResult> AddCoOwner(string name, decimal balance, decimal monthlyFee)
        {
            var coOwner = new CoOwner()
            {
                Name = name,
                Balance = balance,
                MonthlyFee = monthlyFee
            };
            if (ModelState.IsValid)
            {
                await _unitOfWork.CoOwner.Add(coOwner);
                await _unitOfWork.CompleteAsync();

                return Ok(coOwner);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        
        //[HttpDelete("{CoOwnerId}")]
        //public async Task<IActionResult> DeleteCoOwner(int CoOwnerId)
        //{
        //    var coOwner = await _unitOfWork.CoOwner.GetById(CoOwnerId);
        //    if (coOwner == null)
        //        return BadRequest();
        //    await _unitOfWork.CoOwner.Delete(CoOwnerId);
        //    await _unitOfWork.CompleteAsync();

        //    return Ok(coOwner);
        //}

        [HttpDelete("{CoOwnerId}")]
        public async Task<IActionResult> DeleteCoOwner(int CoOwnerId)
        {
            var CoOwner = await _unitOfWork.CoOwner.GetById(CoOwnerId);
            
            if (CoOwner == null)
                return BadRequest("inexistant CoOwner");

            var Transactions = await _unitOfWork.MonthDetail.GetAllTransByCo(CoOwnerId);

            if (Transactions != null)
            {
                await _unitOfWork.MonthDetail.DeleteAllTransByCo(Transactions);
            }
            await _unitOfWork.CoOwner.Delete(CoOwnerId);
            await _unitOfWork.CompleteAsync();

            return Ok("deleted succesfully");
        }


    }
}
