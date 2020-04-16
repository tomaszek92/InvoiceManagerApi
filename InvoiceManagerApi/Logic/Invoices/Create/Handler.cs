using InvoiceManagerApi.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Invoices.Create
{
    public class Handler : IRequestHandler<Command, Invoice>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Invoice> Handle(Command request, CancellationToken cancellationToken)
        {
            await _dbContext.Invoices.AddAsync(request.Invoice, cancellationToken);
            await _dbContext.SaveChangesAsync();

            return request.Invoice;
        }
    }
}
