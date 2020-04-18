using InvoiceManagerApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var invoiceRowsToDelete = await _dbContext
                .InvoiceRows
                .Where(row => row.InvoiceId == request.Invoice.Id)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            _dbContext.Entry(request.Invoice).State = EntityState.Modified;

            foreach (var row in request.Invoice.Rows)
            {
                if (row.Id != default)
                {
                    _dbContext.Entry(row).State = EntityState.Modified;
                    invoiceRowsToDelete.RemoveAll(ir => ir.Id == row.Id);
                }
                else
                {
                    _dbContext.Entry(row).State = EntityState.Added;
                }
            }

            foreach (var row in invoiceRowsToDelete)
            {
                _dbContext.Entry(row).State = EntityState.Deleted;
            }

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
