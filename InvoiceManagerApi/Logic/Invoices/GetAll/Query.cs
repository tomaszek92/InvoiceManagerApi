using InvoiceManagerApi.Models;
using MediatR;
using System.Collections.Generic;

namespace InvoiceManagerApi.Logic.Invoices.GetAll
{
    public class Query : IRequest<List<Invoice>>
    {
    }
}
