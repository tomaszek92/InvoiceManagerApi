using InvoiceManagerApi.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Clients.GetById
{
    public class Handler : IRequestHandler<Query, Client>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.FindAsync(request.Id, cancellationToken);
        }
    }
}
