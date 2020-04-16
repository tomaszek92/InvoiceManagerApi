using InvoiceManagerApi.Models;
using MediatR;

namespace InvoiceManagerApi.Logic.Invoices.Update
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
