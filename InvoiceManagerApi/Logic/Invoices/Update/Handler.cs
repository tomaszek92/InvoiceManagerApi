using InvoiceManagerApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Invoices.Update
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
            _dbContext.Entry(request.Invoice).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await InvoiceExistsAsync(request.Invoice.Id, cancellationToken))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return request.Invoice;
        }

        private async Task<bool> InvoiceExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Invoices.AnyAsync(e => e.Id == id);
        }
    }
}
