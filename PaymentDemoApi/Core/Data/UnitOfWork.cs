using PaymentDemoApi.Core.IConfiguration;
using PaymentDemoApi.Core.Repositories;
using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.Data
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly PaymentDemoContext _context;
        private readonly ILogger _logger;
        public ICoOwnerRepository CoOwners { get; private set; }

        public IMonthDetailRepository MonthDetails {get; private set; }

        public UnitOfWork(PaymentDemoContext context,ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            CoOwners = new CoOwnerRepository(_context, _logger);
            MonthDetails= new MonthDetailRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public  void Dispose()
        {
             _context.Dispose();
        }

        
    }
}
