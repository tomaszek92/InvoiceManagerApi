using InvoiceManagerApi.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Clients.Create
{
    public class Handler : IRequestHandler<Command, Client>
    {
        private readonly IApplicationDbContext _dbContext;

        public Handler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> Handle(Command request, CancellationToken cancellationToken)
        {
            await _dbContext.Clients.AddAsync(request.Client, cancellationToken);
            await _dbContext.SaveChangesAsync();

            return request.Client;
        }
    }
}
