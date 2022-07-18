using Microsoft.Extensions.Logging;
using PaymentDemoApi.Core.IConfiguration;

namespace PaymentDemoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoOwner(int id)
        {
            var coOwner = await _unitOfWork.CoOwner.GetById(id);

            if (coOwner == null)
                return NotFound();
            return Ok(coOwner);
        }

        [HttpPost]
        
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

        
        [HttpDelete]
        public async Task<IActionResult> DeleteCoOwner(int id)
        {
            var coOwner = await _unitOfWork.CoOwner.GetById(id);
            if (coOwner == null)
                return BadRequest();
            await _unitOfWork.CoOwner.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(coOwner);
        }

      

        //[HttpGet]
        //public async Task<ActionResult<List<CoOwner>>> GetEveryCoOwner() => Ok(await _context.CoOwners.ToListAsync());


        //[HttpGet("{CoOwnerId}")]
        //public async Task<ActionResult> GetCoOwnerById(int CoOwnerId)
        //{
        //    var RequestedCoOwner = await _context.CoOwners.FirstAsync(x=>x.Id==CoOwnerId);

        //    if (RequestedCoOwner == null) 
        //        return NotFound();

        //    return Ok(RequestedCoOwner);
        //}



        //[HttpPost]
        //public async Task<ActionResult> AddCoOwner(string name,decimal balance,decimal MonthlyFee)
        //{
        //    var NewCoOwner = new CoOwner
        //    {
        //        Name = name,
        //        Balance = balance,
        //        MonthlyFee = MonthlyFee
        //    };

        //    _context.CoOwners.Add(NewCoOwner);
        //    await _context.SaveChangesAsync();

        //    return Ok(NewCoOwner);
        //}



        //[HttpPut]
        //public async Task<ActionResult> EditCoOwner(int idCOToBeModified, string NewName,decimal NewMonthlyFee)
        //{

        //    var CoToBeUpdated = await _context.CoOwners.FirstAsync(x => x.Id == idCOToBeModified);

        //    if (CoToBeUpdated == null)
        //        return BadRequest($"Unable to find a CoOwner with the Id: "+idCOToBeModified);

        //    CoToBeUpdated.Name = NewName;
        //    CoToBeUpdated.MonthlyFee = NewMonthlyFee;

        //     _context.CoOwners.Update(CoToBeUpdated);
        //    await _context.SaveChangesAsync();

        //    return Ok(CoToBeUpdated);
        //}


        //[HttpDelete]
        //public async Task<ActionResult> DeleteCoOwner(int id)
        //{
        //    var CoToBeDeleted = await _context.CoOwners.FirstAsync(x => x.Id == id);

        //    if (CoToBeDeleted == null)
        //        return BadRequest($"Unable to find a CoOwner with the Id: " + id);

        //    _context.CoOwners.Remove(CoToBeDeleted);
        //    await _context.SaveChangesAsync();

        //    return Ok("Deleted successfully");
        //}

    }
}
