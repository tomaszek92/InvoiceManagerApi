using InvoiceManagerApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceManagerApi.Controllers
{
    [Route("api/invoices")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Invoice>>> GetInvoices(CancellationToken cancellationToken)
        {
            var query = new Logic.Invoices.GetAll.Query();
            var invoices = await _mediator.Send(query, cancellationToken);

            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id, CancellationToken cancellationToken)
        {
            var query = new Logic.Invoices.GetById.Query(id);
            var invoice = await _mediator.Send(query, cancellationToken);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, Invoice invoice, CancellationToken cancellationToken)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            var command = new Logic.Invoices.Update.Command(invoice);
            var updatedInvoice = await _mediator.Send(command, cancellationToken);

            return updatedInvoice == null ? NotFound() : (IActionResult)NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice, CancellationToken cancellationToken)
        {
            var command = new Logic.Invoices.Create.Command(invoice);
            var addedInvoice = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetInvoice), new { id = addedInvoice.Id }, invoice);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Invoice>> DeleteInvoice(int id, CancellationToken cancellationToken)
        {
            var command = new Logic.Invoices.Delete.Command(id);
            var invoice = await _mediator.Send(command, cancellationToken);
            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }
    }
}
