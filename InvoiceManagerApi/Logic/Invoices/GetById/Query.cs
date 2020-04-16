using InvoiceManagerApi.Models;
using MediatR;

namespace InvoiceManagerApi.Logic.Invoices.GetById
{
    public class Query : IRequest<Invoice>
    {
        public int Id { get; }

        public Query(int id)
        {
            Id = id;
        }
    }
}
