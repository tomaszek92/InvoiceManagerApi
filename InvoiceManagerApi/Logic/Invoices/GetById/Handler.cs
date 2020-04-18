using InvoiceManagerApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Invoices.GetById
{
    public class Handler : IRequestHandler<Query, Invoice>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Invoice> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Invoices
                .Include(invoice => invoice.Rows)
                .FirstOrDefaultAsync(invoice => invoice.Id == request.Id, cancellationToken);
        }
    }
}
