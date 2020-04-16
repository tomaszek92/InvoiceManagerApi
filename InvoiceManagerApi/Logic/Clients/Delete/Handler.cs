using InvoiceManagerApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Clients.Delete
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
            var client = await _dbContext
                .Clients
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (client == null)
            {
                return null;
            }

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();

            return client;
        }
    }
}
