
namespace PaymentDemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoOwnerManagementController : ControllerBase
    {
        private readonly PaymentDemoContext _context;
        public CoOwnerManagementController(PaymentDemoContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<CoOwner>>> GetEveryCoOwner() => Ok(await _context.CoOwners.ToListAsync());

       
        [HttpGet("{CoOwnerId}")]
        public async Task<ActionResult> GetCoOwnerById(int CoOwnerId)
        {
            var RequestedCoOwner = await _context.CoOwners.FirstAsync(x=>x.Id==CoOwnerId);

            if (RequestedCoOwner == null) 
                return NotFound();
                
            return Ok(RequestedCoOwner);
        }


        [HttpPost]
        public async Task<ActionResult> AddCoOwner(string name,decimal balance,decimal MonthlyFee)
        {
            var NewCoOwner = new CoOwner
            {
                Name = name,
                Balance = balance,
                MonthlyFee = MonthlyFee
            };

            _context.CoOwners.Add(NewCoOwner);
            await _context.SaveChangesAsync();

            return Ok(NewCoOwner);
        }


        [HttpPut]
        public async Task<ActionResult> EditCoOwner(int idCOToBeModified, string NewName,decimal NewMonthlyFee)
        {
            
            var CoToBeUpdated = await _context.CoOwners.FirstAsync(x => x.Id == idCOToBeModified);

            if (CoToBeUpdated == null)
                return BadRequest($"Unable to find a CoOwner with the Id: "+idCOToBeModified);

            CoToBeUpdated.Name = NewName;
            CoToBeUpdated.MonthlyFee = NewMonthlyFee;

             _context.CoOwners.Update(CoToBeUpdated);
            await _context.SaveChangesAsync();

            return Ok(CoToBeUpdated);
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteCoOwner(int id)
        {
            var CoToBeDeleted = await _context.CoOwners.FirstAsync(x => x.Id == id);

            if (CoToBeDeleted == null)
                return BadRequest($"Unable to find a CoOwner with the Id: " + id);

            _context.CoOwners.Remove(CoToBeDeleted);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully");
        }

    }
}
