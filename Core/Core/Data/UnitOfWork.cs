using PaymentDemoApi.Core.IConfiguration;
using PaymentDemoApi.Core.Repositories;
using PaymentDemoApi.IRepositories;

namespace PaymentDemoApi.Core.Data
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly PaymentDemoContext _context;
        private readonly ILogger _logger;
        public ICoOwnerRepository CoOwner { get; private set; }

        public IMonthDetailRepository MonthDetail {get; private set; }

        public UnitOfWork(PaymentDemoContext context,ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            CoOwner = new CoOwnerRepository(_context, _logger);
            MonthDetail= new MonthDetailRepository(_context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
             _context.Dispose();
        }

        
    }
}
