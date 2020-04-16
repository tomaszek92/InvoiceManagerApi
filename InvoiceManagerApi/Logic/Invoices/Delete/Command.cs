using InvoiceManagerApi.Models;
using MediatR;

namespace InvoiceManagerApi.Logic.Invoices.Delete
{
    public class Command : IRequest<Invoice>
    {
        public int Id { get; }

        public Command(int id)
        {
            Id = id;
        }
    }
}
