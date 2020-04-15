using InvoiceManagerApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Clients.Update
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
            _dbContext.Entry(request.Client).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClientExistsAsync(request.Client.Id, cancellationToken))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return request.Client;
        }

        private async Task<bool> ClientExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Clients.AnyAsync(e => e.Id == id);
        }
    }
}
