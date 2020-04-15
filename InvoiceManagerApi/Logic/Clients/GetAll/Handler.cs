using InvoiceManagerApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Clients.GetAll
{
    public class Handler : IRequestHandler<Query, List<Client>>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Client>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.ToListAsync(cancellationToken);
        }
    }
}
