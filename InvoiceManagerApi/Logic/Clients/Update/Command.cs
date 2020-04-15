using InvoiceManagerApi.Models;
using MediatR;

namespace InvoiceManagerApi.Logic.Clients.Update
{
    public class Command : IRequest<Client>
    {
        public Client Client { get; }

        public Command(Client client)
        {
            Client = client;
        }
    }
}
