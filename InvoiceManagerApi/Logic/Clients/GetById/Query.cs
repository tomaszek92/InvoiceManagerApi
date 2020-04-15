using InvoiceManagerApi.Models;
using MediatR;

namespace InvoiceManagerApi.Logic.Clients.GetById
{
    public class Query : IRequest<Client>
    {
        public int Id { get; }

        public Query(int id)
        {
            Id = id;
        }
    }
}
