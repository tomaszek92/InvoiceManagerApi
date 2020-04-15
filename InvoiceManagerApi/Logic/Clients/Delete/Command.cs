using InvoiceManagerApi.Models;
using MediatR;

namespace InvoiceManagerApi.Logic.Clients.Delete
{
    public class Command : IRequest<Client>
    {
        public int Id { get; }

        public Command(int id)
        {
            Id = id;
        }
    }
}
