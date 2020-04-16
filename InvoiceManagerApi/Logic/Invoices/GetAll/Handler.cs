using InvoiceManagerApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Invoices.GetAll
{
    public class Handler : IRequestHandler<Query, List<Invoice>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Invoice>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _dbContext.Invoices.ToListAsync(cancellationToken);
        }
    }
}
