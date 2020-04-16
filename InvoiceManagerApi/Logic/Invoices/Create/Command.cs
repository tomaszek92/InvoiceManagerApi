using InvoiceManagerApi.Models;
using MediatR;

namespace InvoiceManagerApi.Logic.Invoices.Create
{
    public class Command : IRequest<Invoice>
    {
        public Invoice Invoice { get; }

        public Command(Invoice invoice)
        {
            Invoice = invoice;
        }
    }
}
