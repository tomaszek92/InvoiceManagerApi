using InvoiceManagerApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Invoices.Delete
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
            var invoice = await _dbContext
                .Invoices
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (invoice == null)
            {
                return null;
            }

            _dbContext.Invoices.Remove(invoice);
            await _dbContext.SaveChangesAsync();

            return invoice;
        }
    }
}
