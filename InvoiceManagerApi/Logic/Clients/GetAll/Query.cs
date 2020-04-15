using InvoiceManagerApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Logic.Clients.GetAll
{
    public class Query : IRequest<List<Client>>
    {
    }
}
